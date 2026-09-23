namespace Trivo.API.Controllers.V1.Requests;

/// <param name="Decision">"Approved" or "Rejected".</param>
/// <param name="FinalReason">Mandatory explanation of the decision.</param>
/// <param name="SanctionType">Required when approving: "Warning", "TemporarySuspension" or "PermanentBan".</param>
/// <param name="DurationDays">Required for a temporary suspension (1 to 365); not allowed otherwise.</param>
/// <param name="NotifyByEmail">Also email the sanctioned user. Defaults to true.</param>
public sealed record ResolveReportRequest(
    string Decision,
    string FinalReason,
    string? SanctionType = null,
    int? DurationDays = null,
    bool NotifyByEmail = true);
