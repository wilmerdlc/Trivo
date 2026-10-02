using Microsoft.Extensions.Logging;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;

namespace Trivo.Application.Services;

public sealed class AccountAccessService(
    ISanctionRepository sanctionRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILogger<AccountAccessService> logger
) : IAccountAccessService
{
    public async Task<Error?> GetAccessErrorAsync(User user, CancellationToken cancellationToken)
    {
        var isBanned = user.UserStatus == nameof(UserStatus.Banned);
        var isSuspended = user.UserStatus == nameof(UserStatus.Suspended);

        if (!isBanned && !isSuspended)
        {
            return null;
        }

        var now = DateTime.UtcNow;
        var sanction = await sanctionRepository.GetCurrentBlockAsync(user.Id, now, cancellationToken);

        if (sanction is not null)
        {
            return BuildError(
                Enum.Parse<SanctionType>(sanction.Type!),
                sanction.Reason,
                sanction.ExpiresAt,
                now);
        }

        if (isBanned)
        {
            return BuildError(SanctionType.PermanentBan, null, null, now);
        }

        // Suspended, but every suspension on record has expired: lift it.
        user.UserStatus = nameof(UserStatus.Active);
        user.UpdatedAt = now;

        await userRepository.UpdateAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Suspension of user '{UserId}' has expired; account restored to Active.", user.Id);

        return null;
    }

    #region Private Methods
    private static Error BuildError(SanctionType type, string? reason, DateTime? expiresAt, DateTime now)
    {
        var remaining = expiresAt is null ? (TimeSpan?)null : TimeSpan.FromTicks(Math.Max(0, (expiresAt.Value - now).Ticks));
        var isSuspension = type == SanctionType.TemporarySuspension;

        // Shown as-is by the frontend, so it's written in Spanish (the app's UI language).
        var reasonText = string.IsNullOrWhiteSpace(reason) ? string.Empty : $" Motivo: {reason}";

        var message = isSuspension
            ? $"Tu cuenta está suspendida hasta el {expiresAt:dd'/'MM'/'yyyy HH':'mm} UTC (quedan {Humanize(remaining!.Value)}).{reasonText}"
            : $"Tu cuenta ha sido baneada permanentemente.{reasonText}";

        var extensions = new Dictionary<string, object?>
        {
            ["sanctionType"] = type.ToString(),
            ["reason"] = reason,
            ["expiresAt"] = expiresAt,
            ["remainingSeconds"] = remaining is null ? null : (long)remaining.Value.TotalSeconds
        };

        return Error.Conflict(isSuspension ? "Account.Suspended" : "Account.Banned", message)
            .WithExtensions(extensions);
    }

    private static string Humanize(TimeSpan span)
    {
        var parts = new List<string>();

        if (span.Days > 0) parts.Add($"{span.Days} día{(span.Days == 1 ? "" : "s")}");
        if (span.Hours > 0) parts.Add($"{span.Hours} hora{(span.Hours == 1 ? "" : "s")}");
        if (span.Minutes > 0) parts.Add($"{span.Minutes} minuto{(span.Minutes == 1 ? "" : "s")}");

        return parts.Count > 0 ? string.Join(", ", parts) : "menos de un minuto";
    }
    #endregion
}
