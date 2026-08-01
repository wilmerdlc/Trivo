using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;

using Trivo.Application.DTOs.Email;

namespace Trivo.Application.Features.Users.Commands.ForgotPassword;

internal sealed class ForgotPasswordCommandHandler(
    IUserRepository userRepository,
    ICodeService codeService,
    IEmailService emailService,
    ILogger<ForgotPasswordCommandHandler> logger
) : ICommandHandler<ForgotPasswordCommand, string>
{
    public async Task<ResultT<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            logger.LogWarning("No user was found with email '{Email}'.", request.Email);

            return ResultT<string>.Failure(Error.NotFound("404", "No user exists with this email."));
        }

        var code = await codeService.GenerateCodeAsync(user.Id, CodeType.PasswordRecovery, cancellationToken);

        if (!code.IsSuccess)
        {
            logger.LogError(
                "Failed to generate the recovery code for user '{UserId}'. Error: {Error}",
                user.Id,
                code.Error!.Description
            );

            return ResultT<string>.Failure(code.Error!);
        }

        await emailService.SendEmailAsync(
            new EmailResponseDto(
                User: user.Email!,
                Body: EmailTemplate.PasswordRecovery(user.Username!, code.Value),
                Subject: "Forgot your password"
            )
        );

        logger.LogInformation(
            "Recovery code sent to email '{Email}' for user '{UserId}'.",
            user.Email,
            user.Id
        );

        return ResultT<string>.Success("The recovery code has been sent successfully.");
    }
}
