using Kusys.Business.Abstract;
using Kusys.Core.Concrete;
using Kusys.Entities.Concrete;
using Kusys.Entities.Dto;
using Kusys.Entities.ViewModel;
using Kusys.WebUI.Filters;
using Kusys.WebUI.Models;
using Kusys.WebUI.Models.NotifyMessage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Kusys.WebUI.Controllers;

[Auth]
public class StudentController : Controller
{
    private readonly IStudentService _studentService;
    private readonly ICourseService _courseService;
    private IStudentCourseMappingService _studentCourseMappingService;

    public StudentController(IStudentService studentService, IStudentCourseMappingService studentCourseMappingService,
        ICourseService courseService)
    {
        _studentService = studentService;
        _studentCourseMappingService = studentCourseMappingService;
        _courseService = courseService;
    }

    #region UserRole

    
    [Route("student-course-list")]
    [AuthUser]
    public IActionResult StudentCourseList()
    {
        var currentUser = _studentService.GetStudentByEmail(CurrentSession.User.Email);
        var students = _studentService.GetAllStudentQueryable()
            .Include(s => s.StudentCourseMapping)
            .ThenInclude(sc => sc.Course)
            .ThenInclude(c => c.CourseCategory)
            .ToList().SingleOrDefault(x=>x.Id==currentUser.Result.Id);

        
        var studentViewModels = new List<StudentViewModel>
        {
            new StudentViewModel
            {
                // CourseId, öğrencinin aldığı derslerin kimlik bilgisi ve kategori adı ile birleştirilmiş hali olarak atanıyor.
                CourseId = students.StudentCourseMapping
                    .Select(sc => $"{sc.Course.CourseCategory.Name} {sc.Course.CourseId}").ToList(),
        
                // CourseName, öğrencinin aldığı derslerin isimleri olarak atanıyor.
                CourseName = students.StudentCourseMapping
                    .Select(sc => $"{sc.Course.Name}").ToList()
            }
        };
        
        
        return View(studentViewModels);
    }
    
    [Route("student-profile")]
    [AuthUser]
    public IActionResult StudentProfile()
    {
        var currentUser = _studentService.GetStudentByEmail(CurrentSession.User.Email);
        StudentViewModel? studentViewModel = null;
        if (currentUser == null) return Redirect("/");
        var students = _studentService.GetAllStudentQueryable()
            .Include(s => s.StudentCourseMapping)
            .ThenInclude(sc => sc.Course)
            .ThenInclude(c => c.CourseCategory)
            .ToList().SingleOrDefault(x => x.Email == currentUser.Result.Email);


        if (students != null)
        {
            studentViewModel = new StudentViewModel()
            {
                StudentId = students.Id,
                FirstName = students.FirstName,
                LastName = students.LastName,
                Email = students.Email,
                TrIdentityNumber = students.TrIdentityNumber,
                BirthDate = students.BirthDate,
                Courses = students.StudentCourseMapping
                    .Select(sc => $"{sc.Course.CourseCategory.Name} {sc.Course.CourseId} {sc.Course.Name}").ToList()
            };
        }
        else
        {
            var studentInfo = _studentService.GetAllStudent().SingleOrDefault(x => x.Email == currentUser.Result.Email);
            students = studentInfo;

            if (students != null)
                studentViewModel = new StudentViewModel()
                {
                    StudentId = students.Id,
                    FirstName = students.FirstName,
                    LastName = students.LastName,
                    Email = students.Email,
                    TrIdentityNumber = students.TrIdentityNumber,
                    BirthDate = students.BirthDate,
                    Courses = null
                };
        }

        return View(studentViewModel);
    }

    [Route("student-course-add")]
    [AuthUser]
    public IActionResult StudentCourseAdd()
    {
        var currentUser = _studentService.GetStudentByEmail(CurrentSession.User.Email);
        ViewBag.Courses = _courseService.GetCourses().Include("CourseCategory").ToList();
        ViewBag.Course = _courseService.GetCourses().ToList();
        ViewBag.ThisStudentCourses = _studentCourseMappingService.GetStudentCourseMappingCategory()
            .Where(x => x.StudentId == currentUser.Result.Id).ToList();

        return View();
    }

    [Route("student-course-add")]
    [AuthUser]
    [HttpPost]
    public IActionResult StudentCourseAdd(List<int> tags)
    {
        var currentUser = _studentService.GetStudentByEmail(CurrentSession.User.Email);

        var dataTags = _studentCourseMappingService.GetStudentCourseMappingCategory()
            .Where(x => x.StudentId == currentUser.Result.Id).ToList();
        if (dataTags.Count > 0)
        {
            foreach (var t in dataTags)
            {
                _studentCourseMappingService.Delete(t);
            }
        }

        if (!tags.Any()) return Redirect("/student-profile");
        foreach (var mapping in tags.Select(tagId => new StudentCourseMapping
                 {
                     CourseId = tagId,
                     StudentId = currentUser.Result.Id
                 }))
        {
            _studentCourseMappingService.Insert(mapping);
        }

        return Redirect("/student-profile");
    }

    #endregion

    #region AdminRole

    [Route("student-list")]
    [AuthAdmin]
    public IActionResult Index()
    {
        return View(_studentService.GetAllStudent());
    }

    [Route("students-course-list")]
    [AuthAdmin]
    public IActionResult StudentsCourseList()
    {
        var students = _studentService.GetAllStudentQueryable()
            .Include(s => s.StudentCourseMapping)
            .ThenInclude(sc => sc.Course)
            .ThenInclude(c => c.CourseCategory)
            .ToList();

        var studentViewModels = students.Select(student => new StudentViewModel
        {
            StudentId = student.Id,
            StudentFullname = student.FirstName + " " + student.LastName,
            Email = student.Email,
            TrIdentityNumber = student.TrIdentityNumber,
            BirthDate = student.BirthDate,
            Courses = student.StudentCourseMapping
                .Select(sc => $"{sc.Course.CourseCategory.Name}{sc.Course.CourseId} {sc.Course.Name}").ToList()
        }).ToList();


        //
        // List<StudentCourseMapping> data = _studentCourseMappingService.GetStudentCourseMappingCategory();
        //
        // var students = new List<Student>();
        // var courses = new List<Course>();
        //
        //
        // foreach (var item in data)
        // {
        //     
        //     var students = _studentService.GetAllStudentQueryable().Include(s => s.StudentCourseMapping).ThenInclude(sc => sc.CourseCategory)
        //         .FirstOrDefault(s => s.Id == item.StudentId);
        //     
        //     var studentViewModels = students.Select(student => new StudentViewModel
        //     {
        //         StudentFullname = student.FirstName + " " + student.LastName ,
        //         Courses = student.StudentCourseMapping.Select(sc => sc.CourseCategory.Name).ToList()
        //     }).ToList();
        //     
        //     // var course = student.StudentCourseMapping.Select(sc => sc.CourseCategory);
        //     
        //     
        // }
        //
        // StudentCourseMappingVm StudentCourseMappingVm = new StudentCourseMappingVm()
        // {
        //     Student = null,
        //     Courses = null
        // };
        return View(studentViewModels);
    }

    [Route("student-create")]
    [AuthAdmin]
    public IActionResult Create()
    {
        ViewBag.Courses = _courseService.GetCourses().Include("CourseCategory").ToList();
        return View();
    }

    [Route("student-create")]
    [HttpPost]
    [AuthAdmin]
    public IActionResult Create(Student model, string password, List<int> tags)
    {
        // ModelState'den belirli alanları kaldırarak, bu alanların doğrulama hatalarını temizliyoruz.
        // Bu adımla, "CreatedDate", "ModifiedDate" ve "ModifiedUsername" alanlarının doğrulama hataları
        // ModelState üzerinden kaldırılmış olur.
        ModelState.Remove("CreatedDate");
        ModelState.Remove("ModifiedDate");
        ModelState.Remove("ModifiedUsername");


        var data = _studentService.Insert(model, password);
        if (data.Errors.Count > 0)
        {
            data.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
            return View(data.Result);
        }

        if (!tags.Any()) return Redirect("/student-list");
        foreach (var mapping in tags.Select(tagId => new StudentCourseMapping()
                 {
                     CourseId = tagId,
                     StudentId = data.Result.Id
                 }))
        {
            _studentCourseMappingService.Insert(mapping);
        }

        return Redirect("/student-list");
    }

    [Route("student-edit/{id}")]
    [AuthAdmin]
    public IActionResult Edit(int id)
    {
        if (id == null)
        {
            return Redirect("/student-list");
        }

        var checkData = _studentService.GetStudentById(id);
        if (checkData.Errors.Count > 0)
        {
            checkData.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
            return Redirect("/student-list");
        }


        ViewBag.Course = _courseService.GetCourses().ToList();
        ViewBag.ThisStudentCourses = _studentCourseMappingService.GetStudentCourseMappingCategory()
            .Where(x => x.StudentId == checkData.Result.Id).ToList();


        return View(checkData.Result);
    }

    [Route("student-edit/{id}")]
    [HttpPost]
    [AuthAdmin]
    public IActionResult Edit(Student model, List<int> tags)
    {
        ModelState.Remove("CreatedDate");
        ModelState.Remove("ModifiedDate");
        ModelState.Remove("ModifiedUsername");

        ViewBag.Course = _courseService.GetCourses().ToList();
        ViewBag.ThisStudentCourses = _studentCourseMappingService.GetStudentCourseMappingCategory()
            .Where(x => x.StudentId == model.Id).ToList();
    
        var editResult = _studentService.Update(model);
        if (editResult.Errors.Count > 0)
        {
            editResult.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
            return View(editResult.Result);
        }


        var dataTags = _studentCourseMappingService.GetStudentCourseMappingCategory()
            .Where(x => x.StudentId == editResult.Result.Id).ToList();

        if (dataTags != null)
        {
            foreach (var t in dataTags)
            {
                _studentCourseMappingService.Delete(t);
            }
        }

        if (!tags.Any()) return Redirect("/student-list");
        foreach (var mapping in tags.Select(tagId => new StudentCourseMapping
                 {
                     CourseId = tagId,
                     StudentId = editResult.Result.Id
                 }))
        {
            _studentCourseMappingService.Insert(mapping);
        }


        return Redirect("/student-list");
        //BUNU DA KULLANBLİRİZ HİZMET OLSUN
        // OkViewModel notifyObj = new OkViewModel()
        // {
        //     Title = "Güncelleme Başarılı",
        //     RedirectingUrl = "/student-list",
        // };
        // notifyObj.Items.Add("Başarıyla güncellendi");
        //
        // return View("Ok", notifyObj);
    }

    [Route("student-delete/{id}")]
    [AuthAdmin]
    public IActionResult Delete(int id)
    {
        var updateResult = _studentService.Delete(id);
        if (updateResult.Errors.Count > 0)
        {
            updateResult.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
        }

        return Redirect("/student-list");
    }

    #endregion
}