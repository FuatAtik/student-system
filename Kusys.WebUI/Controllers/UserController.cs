using Kusys.Business.Abstract;
using Kusys.Core;
using Kusys.Entities.Concrete;
using Kusys.WebUI.Filters;
using Kusys.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kusys.WebUI.Controllers;

[Auth]
public class UserController : Controller
{
    private IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [Route("user-list")]
    public IActionResult Index()
    {
        var data = _userService.GetAllUser().Include(x=>x.Role).ToList();
        return View(data);
    }
}