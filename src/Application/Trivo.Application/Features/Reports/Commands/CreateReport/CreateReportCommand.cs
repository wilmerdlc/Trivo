using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Reports;

namespace Trivo.Application.Features.Reports.Commands.CreateReport;

public sealed record CreateReportCommand(
    Guid ReportedById,
    Guid? MessageId,
    string Note,
    Guid? ReportedUserId = null,
    string? ReportType = null
) : ICommand<ReportDto>, IUserOwnedRequest
{
    Guid IUserOwnedRequest.UserId => ReportedById;
}
