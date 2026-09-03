using MediatR;

namespace Trivo.Application.Features.Users.Events;

/// <summary>
/// Raised whenever a change to a user's profile (bio, interests, skills) may have altered its
/// semantic meaning, so its embedding needs to be regenerated. Handlers that mutate profile data
/// publish this instead of calling <see cref="Interfaces.Services.IEmbeddingService"/> directly,
/// so the AI dependency stays out of every one of those handlers.
/// </summary>
public sealed record UserProfileChangedEvent(Guid UserId) : INotification;
