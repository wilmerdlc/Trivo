using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Reports;

namespace Trivo.Application.Features.Reports.Commands.ResolveReport;
public sealed record ResolveReportCommand(
    Guid ReportId,
    Guid AdminId,
    string Decision,
    string FinalReason,
    string? SanctionType,
    int? DurationDays,
    bool NotifyByEmail
) : ICommand<ReportResolutionDto>;
