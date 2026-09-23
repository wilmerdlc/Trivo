using Trivo.Domain.Common;

namespace Trivo.Domain.Models;

/// <summary>
/// A disciplinary action applied to a user. The table doubles as the history of administrative
/// actions — rows are never deleted, only revoked.
/// </summary>
public sealed class Sanction : BaseEntity
{
    public Guid? UserId { get; set; }

    /// <summary>The report that led to this sanction, if any.</summary>
    public Guid? ReportId { get; set; }

    /// <summary>The administrator who applied it.</summary>
    public Guid? AdminId { get; set; }

    /// <summary>See <see cref="Enums.SanctionType"/>.</summary>
    public string? Type { get; set; }

    public string? Reason { get; set; }

    /// <summary>Set only for temporary suspensions; null means it doesn't expire.</summary>
    public DateTime? ExpiresAt { get; set; }

    /// <summary>Set when an administrator lifts the sanction early (e.g. unban).</summary>
    public DateTime? RevokedAt { get; set; }

    public User? User { get; set; }

    public Report? Report { get; set; }

    public Administrator? Admin { get; set; }
}
