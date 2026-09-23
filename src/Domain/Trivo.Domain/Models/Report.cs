namespace Trivo.Domain.Models;

public sealed class Report
{
    public Guid? ReportId { get; set; }

    /// <summary>The user who filed the report.</summary>
    public Guid? ReportedById { get; set; }

    /// <summary>The user being reported.</summary>
    public Guid? ReportedUserId { get; set; }

    /// <summary>See <see cref="Enums.ReportType"/>.</summary>
    public string? ReportType { get; set; }

    /// <summary>Only set for message reports.</summary>
    public Guid? MessageId { get; set; }

    /// <summary>See <see cref="Enums.ReportStatus"/>.</summary>
    public string? ReportStatus { get; set; }

    public string? Note { get; set; }

    /// <summary>
    /// Exact content that was reported, captured at report time (message text/URL, or a profile
    /// snapshot) so the evidence survives the reported user editing or deleting it afterwards.
    /// </summary>
    public string? ReportedContent { get; set; }

    /// <summary>For message reports, the message type (Text, Image, File).</summary>
    public string? ReportedContentType { get; set; }

    /// <summary>The administrator's mandatory explanation, set when the report is resolved.</summary>
    public string? FinalReason { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ReviewedAt { get; set; }

    public Guid? ReviewedByAdminId { get; set; }

    public Message? Message { get; set; }

    public User? Reporter { get; set; }

    public User? ReportedUser { get; set; }

    public Administrator? ReviewedByAdmin { get; set; }

    public Sanction? Sanction { get; set; }
}
