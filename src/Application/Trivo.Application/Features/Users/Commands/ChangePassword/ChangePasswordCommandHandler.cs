using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

namespace Trivo.Application.Features.Users.Commands.ChangePassword;

internal sealed class ChangePasswordCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILogger<ChangePasswordCommandHandler> logger
) : ICommandHandler<ChangePasswordCommand, string>
{
    public async Task<ResultT<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            logger.LogWarning("No user was found with email '{Email}'.", request.Email);

            return ResultT<string>.Failure(Error.NotFound("404", "User not found."));
        }

        var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

        await userRepository.UpdatePasswordAsync(user, newPasswordHash, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("The password for user with ID '{UserId}' was updated successfully.", user.Id);

        return ResultT<string>.Success("The password has been updated successfully.");
    }
}
