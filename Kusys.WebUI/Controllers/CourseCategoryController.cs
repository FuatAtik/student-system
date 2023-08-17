using Kusys.Business.Abstract;
using Kusys.Entities.Concrete;
using Kusys.WebUI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kusys.WebUI.Controllers;

[Auth]
public class CourseCategoryController : Controller
{
    private readonly ICourseCategoryService _courseCategoryService;

    public CourseCategoryController(ICourseCategoryService courseCategoryService)
    {
        _courseCategoryService = courseCategoryService;
    }

    [Route("course-category-list")]
    public IActionResult Index()
    {
        var data = _courseCategoryService.GetCourseCategory();
        return View(data);
    }

    [Route("course-category-create")]
    public IActionResult Create()
    {
        return View();
    }

    [Route("course-category-create")]
    [HttpPost]
    public IActionResult Create(CourseCategory model)
    {
        ModelState.Remove("CreatedDate");
        ModelState.Remove("ModifiedDate");
        ModelState.Remove("ModifiedUsername");
        try
        {
           var result = _courseCategoryService.Insert(model);
           switch (result)
           {
               case 0:
                   ModelState.AddModelError("", "Registration Failed");
                   return View();
           }
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", "Registration Failed");
        }

        return Redirect("/course-category-list");
    }

    [Route("course-category-edit/{id}")]
    public IActionResult Edit(int id)
    {
        if (id == null)
        {
            return Redirect("/course-category-list");
        }

        var checkData = _courseCategoryService.GetCourseCategoryById(id);
        
        if (checkData != null)
        {
            return View(checkData);
        }

        return Redirect("/course-category-list");
    }

    [Route("course-category-edit/{id}")]
    [HttpPost]
    public IActionResult Edit(CourseCategory model)
    {
        ModelState.Remove("CreatedDate");
        ModelState.Remove("ModifiedDate");
        ModelState.Remove("ModifiedUsername");
        try
        {
            var result = _courseCategoryService.Update(model);
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

        return Redirect("/course-category-list");
    }

    [Route("course-category-delete/{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var data = _courseCategoryService.GetCourseCategoryById(id);
            _courseCategoryService.Delete(data);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", "Delete Failed");
        }

        return Redirect("/course-category-list");
    }
}