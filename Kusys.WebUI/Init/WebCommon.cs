using Kusys.Core.Abstract;
using Kusys.Entities.Concrete;
using Kusys.WebUI.Models;

namespace Kusys.WebUI.Init;

public class WebCommon: ICommon
{
    public string GetCurrentUsername()
    {
        User user = CurrentSession.User;

        if (user != null)
            return user.Username;
        else
            return "system";
    }
}