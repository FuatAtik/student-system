using Kusys.Core.Abstract;

namespace Kusys.Core.Concrete;

public class Common : ICommon
{
    public string GetCurrentUsername()
    {
        return "System";
    }
}