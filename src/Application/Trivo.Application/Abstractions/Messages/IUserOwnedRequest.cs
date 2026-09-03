namespace Trivo.Application.Abstractions.Messages;

/// <summary>
/// Marks a request as scoped to a specific user's own resource. AuthorizationBehavior denies the
/// request unless the authenticated caller's ID matches <see cref="UserId"/>.
/// </summary>
public interface IUserOwnedRequest
{
    Guid UserId { get; }
}
