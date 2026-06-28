using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Trivo.Application.Interfaces.Services;
using Trivo.Domain.Configurations;

using Trivo.Application.DTOs.Email;

namespace Trivo.Infrastructure.Shared.Services;

public class EmailService(IOptions<EmailSetting> emailOptions) : IEmailService
{
    private EmailSetting EmailSettings { get; } = emailOptions.Value;

    public async Task SendEmailAsync(EmailResponseDto emailResponse)
    {
        try
        {
            MimeMessage email = new();
            email.Sender = MailboxAddress.Parse(EmailSettings.EmailFrom);
            email.To.Add(MailboxAddress.Parse(emailResponse.User));
            email.Subject = emailResponse.Subject;

            BodyBuilder builder = new()
            {
                HtmlBody = emailResponse.Body
            };

            email.Body = builder.ToMessageBody();

            // SMTP configuration
            using MailKit.Net.Smtp.SmtpClient smtp = new();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

            await smtp.ConnectAsync(
                EmailSettings.SmtpHost,
                EmailSettings.SmtpPort,
                SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync(
                EmailSettings.SmtpUser,
                EmailSettings.SmtpPassword
            );

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}