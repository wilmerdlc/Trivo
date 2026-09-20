using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;

using Trivo.Application.DTOs.Email;

namespace Trivo.Application.Features.Users.Commands.RequestEmailChange;

internal sealed class RequestEmailChangeCommandHandler(
    IUserRepository userRepository,
    ICodeService codeService,
    IEmailService emailService,
    IUnitOfWork unitOfWork,
    ILogger<RequestEmailChangeCommandHandler> logger
) : ICommandHandler<RequestEmailChangeCommand, string>
{
    public async Task<ResultT<string>> Handle(RequestEmailChangeCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("No user was found with ID '{UserId}'.", request.UserId);

            return ResultT<string>.Failure(Error.NotFound("404", "User not found."));
        }

        if (string.Equals(user.Email, request.NewEmail, StringComparison.OrdinalIgnoreCase))
        {
            logger.LogWarning("User '{UserId}' requested to change their email to their current email.", user.Id);

            return ResultT<string>.Failure(Error.Validation("400", "This is already your current email."));
        }

        if (await userRepository.IsEmailInUseAsync(request.NewEmail, request.UserId, cancellationToken))
        {
            logger.LogWarning("Email '{Email}' is already in use by another user.", request.NewEmail);

            return ResultT<string>.Failure(
                Error.Conflict("409", "This email is already in use by another user.")
            );
        }

        user.PendingEmail = request.NewEmail;
        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.UpdateAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var code = await codeService.GenerateCodeAsync(user.Id, CodeType.EmailChange, cancellationToken);
        if (!code.IsSuccess)
        {
            logger.LogError(
                "Failed to generate an email-change code for user '{UserId}'. Error: {Error}",
                user.Id,
                code.Error!.Description
            );

            return ResultT<string>.Failure(code.Error!);
        }

        await emailService.SendEmailAsync(
            new EmailResponseDto(
                User: request.NewEmail,
                Body: EmailTemplate.ConfirmEmailChange(user.Username!, code.Value),
                Subject: "Confirm your new email address"
            )
        );

        logger.LogInformation(
            "Email-change requested for user '{UserId}'. Confirmation code sent to '{NewEmail}'.",
            user.Id,
            request.NewEmail
        );

        return ResultT<string>.Success("A confirmation code has been sent to your new email address.");
    }
}
