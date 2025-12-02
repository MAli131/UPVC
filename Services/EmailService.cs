using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

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

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(fromName ?? "EMAPEN", fromEmail ?? smtpUsername ?? ""));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                if (isHtml)
                {
                    bodyBuilder.HtmlBody = body;
                }
                else
                {
                    bodyBuilder.TextBody = body;
                }
                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                
                // Connect to SMTP server
                // Try STARTTLS first (port 587), then SSL (port 465), then unencrypted (port 25)
                if (smtpPort == 465)
                {
                    await client.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.SslOnConnect);
                }
                else if (smtpPort == 587)
                {
                    await client.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
                }
                else
                {
                    await client.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.Auto);
                }

                // Authenticate
                await client.AuthenticateAsync(smtpUsername, smtpPassword);

                // Send email
                await client.SendAsync(message);

                // Disconnect
                await client.DisconnectAsync(true);

                _logger.LogInformation("Email sent successfully to {Email}", toEmail);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}. Error: {ErrorMessage}", toEmail, ex.Message);
                return false;
            }
        }

        public async Task<bool> SendContactFormEmailAsync(string name, string email, string content, string? category = null, string? country = null, string? city = null, string? telephone = null)
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
                        {(string.IsNullOrEmpty(country) ? "" : $@"
                        <tr>
                            <td style='padding: 10px; border: 1px solid #ddd; background-color: #f2f2f2;'><strong>Country:</strong></td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{country}</td>
                        </tr>
                        ")}
                        {(string.IsNullOrEmpty(city) ? "" : $@"
                        <tr>
                            <td style='padding: 10px; border: 1px solid #ddd; background-color: #f2f2f2;'><strong>City:</strong></td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{city}</td>
                        </tr>
                        ")}
                        {(string.IsNullOrEmpty(telephone) ? "" : $@"
                        <tr>
                            <td style='padding: 10px; border: 1px solid #ddd; background-color: #f2f2f2;'><strong>Telephone:</strong></td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{telephone}</td>
                        </tr>
                        ")}
                        <tr>
                            <td style='padding: 10px; border: 1px solid #ddd; background-color: #f2f2f2;'><strong>Message:</strong></td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{content}</td>
                        </tr>
                        <tr>
                            <td style='padding: 10px; border: 1px solid #ddd; background-color: #f2f2f2;'><strong>Submitted At:</strong></td>
                            <td style='padding: 10px; border: 1px solid #ddd;'>{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new System.Globalization.CultureInfo("en-US"))}</td>
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
