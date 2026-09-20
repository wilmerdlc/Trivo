using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Email;

namespace Trivo.Application.Features.Users.Commands.ConfirmEmailChange;

internal sealed class ConfirmEmailChangeCommandHandler(
    IUserRepository userRepository,
    ICodeService codeService,
    IEmailService emailService,
    IUnitOfWork unitOfWork,
    ILogger<ConfirmEmailChangeCommandHandler> logger
) : ICommandHandler<ConfirmEmailChangeCommand, string>
{
    public async Task<ResultT<string>> Handle(ConfirmEmailChangeCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("No user was found with ID '{UserId}'.", request.UserId);

            return ResultT<string>.Failure(Error.NotFound("404", "User not found."));
        }

        if (string.IsNullOrEmpty(user.PendingEmail))
        {
            logger.LogWarning("User '{UserId}' has no pending email change to confirm.", user.Id);

            return ResultT<string>.Failure(Error.Validation("400", "There is no pending email change to confirm."));
        }

        var codeValidation = await codeService.ValidateEmailChangeCodeAsync(user.Id, request.Code, cancellationToken);
        if (!codeValidation.IsSuccess)
        {
            logger.LogWarning("Verification code '{Code}' is not valid for user '{UserId}'.", request.Code, user.Id);

            return ResultT<string>.Failure(codeValidation.Error!);
        }

        var oldEmail = user.Email;
        var newEmail = user.PendingEmail;

        user.Email = newEmail;
        user.PendingEmail = null;
        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.UpdateAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Best-effort notice to the address being replaced — a tripwire in case this change
        // wasn't actually initiated by the account owner (e.g. a stolen session).
        if (!string.IsNullOrEmpty(oldEmail))
        {
            await emailService.SendEmailAsync(
                new EmailResponseDto(
                    User: oldEmail,
                    Body: EmailTemplate.EmailChanged(user.Username!, oldEmail, newEmail),
                    Subject: "Your email address was changed"
                )
            );
        }

        logger.LogInformation(
            "Email updated successfully for user '{UserId}' (from '{OldEmail}' to '{NewEmail}').",
            user.Id,
            oldEmail,
            newEmail
        );

        return ResultT<string>.Success("Your email has been updated successfully.");
    }
}
