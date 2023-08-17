using System.Diagnostics;
using Kusys.Business.Abstract;
using Kusys.WebUI.Filters;
using Microsoft.AspNetCore.Mvc;
using Kusys.WebUI.Models;

namespace Kusys.WebUI.Controllers;

[Auth]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Route("")]
    public IActionResult Index()
    {
        return View();
    }
}