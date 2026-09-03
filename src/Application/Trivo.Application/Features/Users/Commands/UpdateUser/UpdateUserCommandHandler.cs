using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Commands.UpdateUser;

internal sealed class UpdateUserCommandHandler(
    IUserRepository userRepository,
    ICacheService cache,
    IUnitOfWork unitOfWork,
    ILogger<UpdateUserCommandHandler> logger
) : ICommandHandler<UpdateUserCommand, UpdateUserDto>
{
    public async Task<ResultT<UpdateUserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("No user was found with ID '{UserId}'.", request.UserId);

            return ResultT<UpdateUserDto>.Failure(Error.NotFound("404", "User not found."));
        }

        if (await userRepository.IsEmailInUseAsync(request.Email, request.UserId, cancellationToken))
        {
            logger.LogWarning("Email '{Email}' is already in use by another user.", request.Email);

            return ResultT<UpdateUserDto>.Failure(
                Error.Conflict("409", "This email is already in use by another user.")
            );
        }

        if (await userRepository.IsUsernameInUseAsync(request.Username, request.UserId, cancellationToken))
        {
            logger.LogWarning("Username '{Username}' is already in use by another user.", request.Username);

            return ResultT<UpdateUserDto>.Failure(
                Error.Conflict("409", "This username is already in use by another user.")
            );
        }

        user.Username = request.Username;
        user.Email = request.Email;
        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.UpdateAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await cache.InvalidateByTagsAsync([CacheKeys.UserTag(user.Id)], cancellationToken);

        logger.LogInformation("User with ID '{UserId}' updated successfully.", user.Id);

        return ResultT<UpdateUserDto>.Success(new UpdateUserDto(
            Username: user.Username,
            Email: user.Email
        ));
    }
}
