using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace RentStudio.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com"; // Your SMTP server
        private readonly int _smtpPort = 587; // Typically 587 or 465 for SSL
        private readonly string _fromEmail = "daria.lapadus17@gmail.com"; // Your email
        private readonly string _fromPassword = "ovzpdtamhbpkdinh"; // Your email password

        public async Task SendEmailWithAttachmentAsync(string toEmail, string subject, string body)
        {
            var message = new MailMessage(_fromEmail, toEmail, subject, body);

            var smtpClient = new SmtpClient(_smtpServer, _smtpPort)
            {
                Credentials = new NetworkCredential(_fromEmail, _fromPassword),
                EnableSsl = true // Typically true for modern email servers
            };

            await smtpClient.SendMailAsync(message);
        }
    }
}
