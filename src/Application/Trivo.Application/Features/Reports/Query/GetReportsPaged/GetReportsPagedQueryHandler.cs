using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;

using Trivo.Application.DTOs.Reports;

namespace Trivo.Application.Features.Reports.Query.GetReportsPaged;

internal sealed class GetReportsPagedQueryHandler(
    IReportRepository reportRepository,
    ILogger<GetReportsPagedQueryHandler> logger
) : IQueryHandler<GetReportsPagedQuery, PagedResult<ReportListItemDto>>
{
    public async Task<ResultT<PagedResult<ReportListItemDto>>> Handle(
        GetReportsPagedQuery request,
        CancellationToken cancellationToken)
    {
        // Normalized to the enum's canonical spelling so the DB comparison is case-exact.
        var status = request.Status is null
            ? null
            : Enum.Parse<ReportStatus>(request.Status, ignoreCase: true).ToString();

        var page = await reportRepository.GetPagedForAdminAsync(status, request.PageNumber, request.PageSize, cancellationToken);

        var items = page.Items!.Select(r => r.ToListItemDto()).ToList();

        logger.LogInformation(
            "Reports retrieved. Status={Status} Page={Page} Size={Size} Total={Total}",
            status ?? "All", request.PageNumber, request.PageSize, page.TotalItems);

        return ResultT<PagedResult<ReportListItemDto>>.Success(
            new PagedResult<ReportListItemDto>(items, page.TotalItems, request.PageNumber, request.PageSize)
        );
    }
}
