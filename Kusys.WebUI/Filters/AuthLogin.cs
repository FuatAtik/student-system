using Kusys.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kusys.WebUI.Filters;

public class AuthLoginAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var currentUser = CurrentSession.User;
        // Eğer mevcut bir kullanıcı oturumu varsa:
        if (currentUser != null)
        {
            // Kullanıcıyı ana sayfaya yönlendiren bir RedirectResult oluşturulur.
            context.Result = new RedirectResult("/");
        }
    }
}