using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;

namespace Trivo.Application.Features.Administrator.Commands.UnbanUser;

/// <summary>
/// Lifts a permanent ban or a temporary suspension early: revokes the user's active sanctions,
/// marks the reports behind them as revoked and restores the account to Active.
/// </summary>
internal sealed class UnbanUserCommandHandler(
    ILogger<UnbanUserCommandHandler> logger,
    IUserRepository userRepository,
    ISanctionRepository sanctionRepository,
    ICacheService cache,
    IUnitOfWork unitOfWork
) : ICommandHandler<UnbanUserCommand, string>
{
    public async Task<ResultT<string>> Handle(UnbanUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            logger.LogInformation("User with ID {UserId} was not found.", request.UserId);

            return ResultT<string>.Failure(
                Error.NotFound("404", "User not found.")
            );
        }

        var wasBanned = user.UserStatus == nameof(UserStatus.Banned);
        var wasSuspended = user.UserStatus == nameof(UserStatus.Suspended);

        if (!wasBanned && !wasSuspended)
        {
            logger.LogInformation("User {UserId} has no ban or suspension to lift ({Status}).", user.Id, user.UserStatus);

            return ResultT<string>.Failure(
                Error.Conflict("409", "The user is not banned or suspended.")
            );
        }

        var now = DateTime.UtcNow;

        // Close every sanction still on record (and its report), so the login check doesn't keep
        // blocking a user an administrator just reinstated.
        var revoked = await sanctionRepository.RevokeActiveAsync(user.Id, now, cancellationToken);

        user.UserStatus = nameof(UserStatus.Active);
        user.UpdatedAt = now;
        await userRepository.UpdateAsync(user, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await cache.InvalidateByTagsAsync([CacheKeys.AdminUsersTag, CacheKeys.UserTag(user.Id)], cancellationToken);

        logger.LogInformation(
            "User with ID {UserId} has been reinstated ({PreviousStatus}). {RevokedCount} sanction(s) revoked.",
            user.Id, wasBanned ? nameof(UserStatus.Banned) : nameof(UserStatus.Suspended), revoked.Count);

        return ResultT<string>.Success(wasBanned
            ? $"User {user.FirstName} - {user.Id} has been unbanned."
            : $"The suspension of user {user.FirstName} - {user.Id} has been revoked."
        );
    }
}
