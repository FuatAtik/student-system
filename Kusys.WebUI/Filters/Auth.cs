using Kusys.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kusys.WebUI.Filters;

public class AuthAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var currentUser = CurrentSession.User;
        // Eğer mevcut bir kullanıcı oturumu yoksa:
        if (currentUser == null)
        {
            // Kullanıcıyı oturum açma sayfasına yönlendiren bir ActionResult oluşturulur.
            context.Result = new RedirectToActionResult("Index", "Login", null);
        }
    }
}