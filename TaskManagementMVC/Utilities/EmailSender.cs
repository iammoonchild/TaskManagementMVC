using System.Net.Mail;
using System.Net;

namespace TaskManagementMVC.Utilities
{
    public class EmailSender
    {
        public static void SendEmail(string email, string body, string subject)
        {

            var client = new SmtpClient("smtp.office365.com", 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("tatva.dotnet.harimatighoghari@outlook.com", "Amee@0412");
            client.EnableSsl = true;

            var message = new MailMessage();
            message.From = new MailAddress("tatva.dotnet.harimatighoghari@outlook.com");
            message.To.Add(email);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            client.Send(message);
        }
    }
}
