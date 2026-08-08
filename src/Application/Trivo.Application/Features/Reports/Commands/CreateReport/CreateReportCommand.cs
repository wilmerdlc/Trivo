using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Reports;

namespace Trivo.Application.Features.Reports.Commands.CreateReport;

public sealed record CreateReportCommand(
    Guid ReportedById,
    Guid MessageId,
    string Note
) : ICommand<ReportDto>;
