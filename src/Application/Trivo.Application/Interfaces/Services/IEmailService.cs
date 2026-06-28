
using Trivo.Application.DTOs.Email;

namespace Trivo.Application.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailResponseDto emailResponse);
}