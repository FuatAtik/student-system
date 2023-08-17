using System.Text.Json;
using Kusys.Business.Abstract;
using Kusys.Core;
using Kusys.Entities.Concrete;
using Kusys.Entities.Dto;
using Kusys.WebUI.Filters;
using Kusys.WebUI.Models;
using Kusys.WebUI.Models.NotifyMessage;
using Microsoft.AspNetCore.Mvc;

namespace Kusys.WebUI.Controllers;


public class LoginController : Controller
{
    private readonly ILoginService _loginService;
    private readonly IUserService _userService;

    public LoginController(ILoginService loginService, IUserService userService)
    {
        _loginService = loginService;
        _userService = userService;
    }

    [AuthLogin]
    [Route("login")]
    public IActionResult Index()
    {
        return View();
    }

    [AuthLogin]
    [Route("login")]
    [HttpPost]
    public IActionResult Index(UserLoginDto model)
    {
        var login = _loginService.Login(model);
        if (login.Errors.Count > 0)
        {
            login.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
            return View(model);
        }
        CurrentSession.Set("login", login.Result);
        return Redirect("/");
    }
    
    [Auth]
    [Route("SessionClear")]
    public IActionResult SessionClear()
    {
        CurrentSession.Remove("login");
        return Redirect("/login");
    }

    [Route("register")]
    public IActionResult Register()
    {
        return View();
    }
    
    
    [HttpPost]
    [Route("register")]
    public IActionResult Register(UserRegisterDto model)
    {
        var register =  _loginService.Register(model);
        if (register.Errors.Count <= 0) return RedirectToAction("Index");
        register.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
        return View(model);
    }
    
    
}