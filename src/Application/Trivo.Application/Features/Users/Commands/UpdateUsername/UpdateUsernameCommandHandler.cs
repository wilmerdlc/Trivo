using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Commands.UpdateUsername;

internal sealed class UpdateUsernameCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILogger<UpdateUsernameCommandHandler> logger
) : ICommandHandler<UpdateUsernameCommand, UpdateUsernameDto>
{
    public async Task<ResultT<UpdateUsernameDto>> Handle(UpdateUsernameCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("No user was found with ID '{UserId}'.", request.UserId);

            return ResultT<UpdateUsernameDto>.Failure(Error.NotFound("404", "User not found."));
        }

        if (await userRepository.IsUsernameInUseAsync(request.Username, request.UserId, cancellationToken))
        {
            logger.LogWarning("Username '{Username}' is already in use by another user.", request.Username);

            return ResultT<UpdateUsernameDto>.Failure(
                Error.Conflict("409", "This username is already in use by another user.")
            );
        }

        user.Username = request.Username;
        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.UpdateAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Username updated successfully for user with ID '{UserId}'.", user.Id);

        return ResultT<UpdateUsernameDto>.Success(new UpdateUsernameDto(
            Username: user.Username
        ));
    }
}
