using MediatR;
using Microsoft.Extensions.Logging;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.UnitOfWork;

namespace Trivo.Application.Features.Users.Events;

/// <summary>
/// Regenerates a user's profile embedding after their profile data changes. Runs after the
/// triggering command has already saved its own changes (see callers), and never lets an
/// embedding-provider failure surface back to the caller — the profile save must succeed
/// independently of the embedding call, per the documented "sync but out of the main
/// transaction" trade-off for this scope.
/// </summary>
internal sealed class UserProfileChangedEventHandler(
    IUserRepository userRepository,
    IEmbeddingService embeddingService,
    IUnitOfWork unitOfWork,
    ILogger<UserProfileChangedEventHandler> logger
) : INotificationHandler<UserProfileChangedEvent>
{
    public async Task Handle(UserProfileChangedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userRepository.GetUserWithInterestsAndSkillsAsync(notification.UserId, cancellationToken);
            if (user is null)
            {
                logger.LogWarning(
                    "Skipped embedding regeneration: no user found with ID {UserId}.",
                    notification.UserId
                );
                return;
            }

            var newHash = UserProfileTextBuilder.Hash(user);
            if (user.ProfileEmbedding is not null && newHash == user.ProfileTextHash)
            {
                logger.LogInformation(
                    "Skipped embedding regeneration for user {UserId}: profile text unchanged since the last embedding.",
                    notification.UserId
                );
                return;
            }

            var profileText = UserProfileTextBuilder.Build(user);
            var embedding = await embeddingService.GetEmbeddingAsync(profileText, cancellationToken);

            await userRepository.UpdateProfileEmbeddingAsync(notification.UserId, new Pgvector.Vector(embedding), newHash, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Profile embedding regenerated for user {UserId}.", notification.UserId);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Failed to regenerate the profile embedding for user {UserId}. The profile itself was already saved; " +
                "the user will be excluded from similarity matches until this succeeds.",
                notification.UserId
            );
        }
    }
}
