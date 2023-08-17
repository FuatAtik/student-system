namespace Kusys.WebUI.Models.NotifyMessage;

public class WarningViewModel : NotifyViewModelBase<string>
{
    public WarningViewModel()
    {
        Title = "Uyarı!";
    }
}