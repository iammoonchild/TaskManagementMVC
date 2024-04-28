using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementMVC.Repositories.Utilities;

public static class EmailSender
{
    public static void SendEmail(string email, string body, string subject)
    {

        var client = new SmtpClient("smtp.gmail.com", 587);
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential("noreply.ci.tatvasoft@gmail.com", "chaz fipw prel ubxv");
        client.EnableSsl = true;

        var message = new MailMessage();
        message.From = new MailAddress("noreply.ci.tatvasoft@gmail.com");
        message.To.Add(email);
        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml = true;
        client.Send(message);
    }

    public static string GetEmailTemplateForInvitation(string userName, string password)
    {
        string emailBody = @"
                    <!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <title>Team Invitation</title>
                    </head>
                    <body style=""font-family: Arial, sans-serif; background-color: #f5f5f5; margin: 0; padding: 0;"">
                        <div style=""max-width: 600px; margin: 20px auto; background-color: #ffffff; border-radius: 10px; overflow: hidden; box-shadow: 0px 0px 20px rgba(0, 0, 0, 0.1);"">
                            <div style=""padding: 20px; text-align: center; background-color: #f5f5f5;"">
                                <img src=""https://i.postimg.cc/QCj9Rk6X/logo-ptm.png"" alt=""Portal Logo"" style=""width: 100px; height: 100px; border-radius: 50%; object-fit: cover; border: 5px solid #ffffff; margin-bottom: 10px;"">
                                <h1 style=""font-size: 24px; margin: 10px 0 0; color: #333333;"">Team Invitation</h1>
                            </div>
                            <div style=""padding: 20px;"">
                                <p style=""font-size: 16px; margin: 0 0 20px; color: #666666;"">You've been invited to join our team!</p>
                                <p style=""font-size: 16px; margin: 0 0 20px; color: #666666;""><strong>Username:</strong>" + userName + @"</p>
                                <p style=""font-size: 16px; margin: 0 0 20px; color: #666666;""><strong>Password:</strong>" + password + @"</p>
                                <p style=""font-size: 16px; margin: 0 0 20px; color: #666666;"">Please click the button below to access the portal:</p>
                                <a href=""https://example.com/portal"" style=""display: inline-block; background-color: #007bff; color: #fff; text-decoration: none; padding: 10px 20px; border-radius: 5px; margin-top: 20px;"">Go to Portal</a>
                            </div>
                        </div>
                    </body>
                    </html>";

        return emailBody;
    }
}
