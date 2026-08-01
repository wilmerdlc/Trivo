using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

namespace Trivo.Application.Features.Users.Commands.UpdatePassword;

internal sealed class UpdatePasswordCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILogger<UpdatePasswordCommandHandler> logger
) : ICommandHandler<UpdatePasswordCommand, string>
{
    public async Task<ResultT<string>> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning(
                "No user was found with ID '{UserId}' while attempting to update the password.",
                request.UserId
            );

            return ResultT<string>.Failure(Error.NotFound("404", "The user does not exist"));
        }

        if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.PasswordHash))
        {
            logger.LogWarning(
                "Password update failed: the old password does not match for user with ID '{UserId}'.",
                user.Id
            );

            return ResultT<string>.Failure(Error.Conflict("409", "The old password is incorrect."));
        }

        var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

        await userRepository.UpdatePasswordAsync(user, newPasswordHash, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Password updated successfully for user with ID '{UserId}'.", user.Id);

        return ResultT<string>.Success("Your password has been updated successfully.");
    }
}
