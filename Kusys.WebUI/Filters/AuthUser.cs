using Kusys.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kusys.WebUI.Filters;

public class AuthUser : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var currentUser = CurrentSession.User;
        // Eğer mevcut kullanıcının rolü "Student" değilse:
        if (currentUser is not { Role.SecretName: "Student" })
        {
            // Kullanıcıyı ana sayfaya yönlendiren bir ActionResult oluşturulur.
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}