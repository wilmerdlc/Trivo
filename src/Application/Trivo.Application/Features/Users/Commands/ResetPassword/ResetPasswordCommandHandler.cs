using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

namespace Trivo.Application.Features.Users.Commands.ResetPassword;

internal sealed class ResetPasswordCommandHandler(
    IUserRepository userRepository,
    ICodeService codeService,
    IUnitOfWork unitOfWork,
    ILogger<ResetPasswordCommandHandler> logger
) : ICommandHandler<ResetPasswordCommand, string>
{
    public async Task<ResultT<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("No user was found with email '{Email}'.", request.Email);

            return ResultT<string>.Failure(Error.NotFound("404", "User not found."));
        }

        var codeValidation = await codeService.ValidatePasswordRecoveryCodeAsync(user.Id, request.Code, cancellationToken);
        if (!codeValidation.IsSuccess)
        {
            logger.LogWarning("Verification code '{Code}' is not valid for user '{UserId}'.", request.Code, user.Id);

            return ResultT<string>.Failure(codeValidation.Error!);
        }

        logger.LogInformation(
            "Code verified successfully for user '{UserId}'. Proceeding to update the password.",
            user.Id
        );

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

        await userRepository.UpdatePasswordAsync(user, passwordHash, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Password updated successfully for user '{UserId}'.", user.Id);

        return ResultT<string>.Success("Password updated successfully.");
    }
}
