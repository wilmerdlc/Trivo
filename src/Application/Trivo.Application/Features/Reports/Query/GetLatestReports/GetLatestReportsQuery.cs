using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Reports;

namespace Trivo.Application.Features.Reports.Query.GetLatestReports;

public sealed record GetLatestReportsQuery : IQuery<IEnumerable<ReportListItemDto>>;
