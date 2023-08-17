using System.Net;
using System.Net.Mail;

namespace Kusys.Core.Helpers;

public class MailHelper
{
    public static bool SendMail(string body, string to, string subject, bool isHtml = true)
    {
        return SendMail(body, new List<string> { to }, subject, isHtml);
    }

    public static bool SendMail(string body, List<string> to, string subject, bool isHtml = true)
    {
        bool result = false;

        try
        {
            var message = new MailMessage();
            message.From = new MailAddress("fuat@grouptaiga.com");

            to.ForEach(x => { message.To.Add(new MailAddress(x)); });

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml;

            using (var smtp = new SmtpClient(
                       "smtp.yandex.ru",
                       587))
            {
                smtp.EnableSsl = true;
                smtp.Credentials =
                    new NetworkCredential(
                        "fuat@grouptaiga.com",
                        "password");

                smtp.Send(message);
                result = true;
            }
        }
        catch (Exception)
        {
        }

        return result;
    }
}