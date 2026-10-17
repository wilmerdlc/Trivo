namespace Trivo.Domain.Enums;

public enum ReportStatus
{
    /// <summary>Legacy value, no longer assigned — kept so existing rows still parse.</summary>
    Resolved,
    Pending,
    Approved,
    Rejected,

    /// <summary>Approved, but its sanction was later lifted early by an administrator.</summary>
    Revoked
}
