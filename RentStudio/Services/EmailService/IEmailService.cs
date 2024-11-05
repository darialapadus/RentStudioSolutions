namespace RentStudio.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailWithAttachmentAsync(string toEmail, string subject, string body);
    }
}
