using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Reports;

namespace Trivo.Application.Features.Reports.Query.GetLatestReports;

internal sealed class GetLatestReportsQueryHandler(
    IReportRepository reportRepository,
    ILogger<GetLatestReportsQueryHandler> logger
) : IQueryHandler<GetLatestReportsQuery, IEnumerable<ReportListItemDto>>
{
    private const int Count = 10;

    public async Task<ResultT<IEnumerable<ReportListItemDto>>> Handle(
        GetLatestReportsQuery request,
        CancellationToken cancellationToken)
    {
        var reports = await reportRepository.GetLatestAsync(Count, cancellationToken);

        logger.LogInformation("Retrieved the latest {Count} reports.", reports.Count);

        // An empty list is a valid answer for a "latest N" listing, not a 404.
        return ResultT<IEnumerable<ReportListItemDto>>.Success(reports.Select(r => r.ToListItemDto()).ToList());
    }
}
