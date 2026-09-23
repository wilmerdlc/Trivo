namespace Trivo.Domain.Enums;

public enum ReportStatus
{
    /// <summary>Legacy value, no longer assigned — kept so existing rows still parse.</summary>
    Resolved,
    Pending,
    Approved,
    Rejected
}
