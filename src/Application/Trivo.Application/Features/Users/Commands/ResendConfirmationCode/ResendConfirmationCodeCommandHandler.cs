using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;

using Trivo.Application.DTOs.Email;

namespace Trivo.Application.Features.Users.Commands.ResendConfirmationCode;

internal sealed class ResendConfirmationCodeCommandHandler(
    IUserRepository userRepository,
    ICodeService codeService,
    IEmailService emailService,
    ILogger<ResendConfirmationCodeCommandHandler> logger
) : ICommandHandler<ResendConfirmationCodeCommand, string>
{
    public async Task<ResultT<string>> Handle(ResendConfirmationCodeCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("Confirmation code resend requested for unregistered email '{Email}'.", request.Email);

            return ResultT<string>.Failure(Error.NotFound("404", "User not found."));
        }

        if (user.IsAccountConfirmed == true)
        {
            logger.LogWarning("Confirmation code resend requested for already-confirmed user '{UserId}'.", user.Id);

            return ResultT<string>.Failure(Error.Conflict("409", "The account has already been confirmed."));
        }

        var code = await codeService.GenerateCodeAsync(user.Id, CodeType.AccountConfirmation, cancellationToken);
        if (!code.IsSuccess)
        {
            logger.LogError(
                "Failed to generate a new confirmation code for user '{UserId}'. Error: {Error}",
                user.Id,
                code.Error!.Description
            );

            return ResultT<string>.Failure(code.Error!);
        }

        await emailService.SendEmailAsync(
            new EmailResponseDto(
                User: user.Email!,
                Body: EmailTemplate.RegisterUser(user.Username!, code.Value),
                Subject: "Confirm your account"
            )
        );

        logger.LogInformation(
            "A new confirmation code was generated and sent to user '{UserId}'.",
            user.Id
        );

        return ResultT<string>.Success("A new confirmation code has been sent to your email.");
    }
}
