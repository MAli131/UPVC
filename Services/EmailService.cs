using System.Net;
using System.Net.Mail;

namespace UPVC.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)
        {
            try
            {
                var emailSettings = _configuration.GetSection("EmailSettings");
                var smtpHost = emailSettings["SmtpHost"];
                var smtpPort = int.Parse(emailSettings["SmtpPort"] ?? "587");
                var smtpUsername = emailSettings["SmtpUsername"];
                var smtpPassword = emailSettings["SmtpPassword"];
                var fromEmail = emailSettings["FromEmail"];
                var fromName = emailSettings["FromName"];
                var enableSsl = bool.Parse(emailSettings["EnableSsl"] ?? "true");

                using var message = new MailMessage();
                message.From = new MailAddress(fromEmail ?? smtpUsername ?? "", fromName ?? "EMAPEN");
                message.To.Add(toEmail);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;

                using var smtpClient = new SmtpClient(smtpHost, smtpPort);
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = enableSsl;

                await smtpClient.SendMailAsync(message);
                _logger.LogInformation("Email sent successfully to {Email}", toEmail);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", toEmail);
                return false;
            }
        }

        public async Task<bool> SendContactFormEmailAsync(string name, string email, string content, string? category = null)
        {
            var adminEmail = _configuration["EmailSettings:AdminEmail"] ?? "info@emapen.net";
            var subject = $"New Contact Form Submission from {name}";
            
            var body = $@"
                <html>
                <body style='font-family: Arial, sans-serif;'>
                    <h2 style='color: #2c5f2d;'>New Contact Form Submission</h2>
                    <table style='border-collapse: collapse; width: 100%;'>
                        <tr>
                            <td style='padding: 10px; border: 1px solid #ddd; background-color: #f2f2f2;'><strong>Name:</strong></td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{name}</td>
                        </tr>
                        <tr>
                            <td style='padding: 10px; border: 1px solid #ddd; background-color: #f2f2f2;'><strong>Email:</strong></td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{email}</td>
                        </tr>
                        {(string.IsNullOrEmpty(category) ? "" : $@"
                        <tr>
                            <td style='padding: 10px; border: 1px solid #ddd; background-color: #f2f2f2;'><strong>Category:</strong></td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{category}</td>
                        </tr>
                        ")}
                        <tr>
                            <td style='padding: 10px; border: 1px solid #ddd; background-color: #f2f2f2;'><strong>Message:</strong></td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{content}</td>
                        </tr>
                        <tr>
                            <td style='padding: 10px; border: 1px solid #ddd; background-color: #f2f2f2;'><strong>Submitted At:</strong></td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{DateTime.Now:yyyy-MM-dd HH:mm:ss}</td>
                        </tr>
                    </table>
                    <p style='margin-top: 20px; color: #666;'>
                        This email was automatically generated from the EMAPEN website contact form.
                    </p>
                </body>
                </html>
            ";

            return await SendEmailAsync(adminEmail, subject, body);
        }
    }
}
