using Kusys.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kusys.WebUI.Filters;

public class AuthAdmin : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var currentUser = CurrentSession.User;
        // Kullanıcının rolü "SuperAdmin" değilse:
        if (currentUser is not { Role.SecretName: "SuperAdmin" })
        {
            // Kullanıcıyı ana sayfaya yönlendiren bir ActionResult oluşturulur.
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}