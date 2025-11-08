namespace UPVC.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true);
        Task<bool> SendContactFormEmailAsync(string name, string email, string content, string? category = null);
    }
}
