using Trivo.Application.Utils;
using Trivo.Domain.Models;

namespace Trivo.Application.Interfaces.Services;

/// <summary>
/// Decides whether a user's account may currently be used, given any ban or suspension applied to it.
/// </summary>
public interface IAccountAccessService
{
    /// <summary>
    /// Returns the error to give the user when their account is blocked by a ban or suspension
    /// (carrying the sanction type, reason and remaining time as structured extensions), or
    /// <see langword="null"/> when the account can be used. A suspension that has already run its
    /// course is lifted here, restoring the account to Active.
    /// </summary>
    /// <param name="user">The user trying to access the platform.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task<Error?> GetAccessErrorAsync(User user, CancellationToken cancellationToken);
}
