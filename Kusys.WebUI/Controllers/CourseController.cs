using Kusys.Business.Abstract;
using Kusys.Entities.Concrete;
using Kusys.WebUI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kusys.WebUI.Controllers;

[Auth]
[AuthAdmin]
public class CourseController : Controller
{
    private readonly ICourseService _courseService;
    private readonly ICourseCategoryService _courseCategoryService;

    public CourseController(ICourseService courseService, ICourseCategoryService courseCategoryService)
    {
        _courseService = courseService;
        _courseCategoryService = courseCategoryService;
    }

    [Route("course-list")]
    public IActionResult Index()
    {
        var data = _courseService.GetCourses().Include("CourseCategory").ToList();
        return View(data);
    }

    [Route("course-create")]
    public IActionResult Create()
    {
        ViewBag.CourseCategoryId = new SelectList(_courseCategoryService.GetCourseCategory(), "Id", "Name");
        return View();
    }

    [Route("course-create")]
    [HttpPost]
    public IActionResult Create(Course model)
    {
        ModelState.Remove("CreatedDate");
        ModelState.Remove("ModifiedDate");
        ModelState.Remove("ModifiedUsername");
        try
        {
            ViewBag.CourseCategoryId = new SelectList(_courseCategoryService.GetCourseCategory(), "Id", "Name");

           var result = _courseService.Insert(model);
           switch (result)
           {
               case 101:
                   ModelState.AddModelError("", "Enter another Id");
                   return View();
               case 0:
                   ModelState.AddModelError("", "Registration Failed");
                   return View();
           }
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", "Registration Failed");
        }

        return Redirect("/course-list");
    }

    [Route("course-edit/{id}")]
    public IActionResult Edit(int id)
    {
        if (id == null)
        {
            return Redirect("/course-list");
        }

        var checkData = _courseService.GetCourseById(id);
        
        if (checkData != null)
        {
            ViewBag.CourseCategoryId = new SelectList(_courseCategoryService.GetCourseCategory(), "Id", "Name", checkData.CourseCategoryId);
            return View(checkData);
        }

        return Redirect("/course-list");
    }

    [Route("course-edit/{id}")]
    [HttpPost]
    public IActionResult Edit(Course model)
    {
        ModelState.Remove("CreatedDate");
        ModelState.Remove("ModifiedDate");
        ModelState.Remove("ModifiedUsername");
        try
        {
            ViewBag.CourseCategoryId = new SelectList(_courseCategoryService.GetCourseCategory(), "Id", "Name", model.CourseCategoryId);
            
            var result = _courseService.Update(model);
           if (result==0)
           {
               ModelState.AddModelError("", "Registration Failed");
               return View(model);
           }
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", "Registration Failed");
        }

        return Redirect("/course-list");
    }

    [Route("course-delete/{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var data = _courseService.GetCourseById(id);
            _courseService.Delete(data);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", "Delete Failed");
        }

        return Redirect("/course-list");
    }
}