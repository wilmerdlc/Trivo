using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Reports;

namespace Trivo.Application.Features.Reports.Query.GetReportById;

public sealed record GetReportByIdQuery(Guid ReportId) : IQuery<ReportDetailDto>;
