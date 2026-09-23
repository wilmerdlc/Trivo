using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Features.Reports;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Reports;

namespace Trivo.Application.Features.Administrator.Query.GetSanctionsHistory;

internal sealed class GetSanctionsHistoryQueryHandler(
    ISanctionRepository sanctionRepository,
    ILogger<GetSanctionsHistoryQueryHandler> logger
) : IQueryHandler<GetSanctionsHistoryQuery, PagedResult<SanctionDto>>
{
    public async Task<ResultT<PagedResult<SanctionDto>>> Handle(
        GetSanctionsHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var page = await sanctionRepository.GetPagedHistoryAsync(
            request.UserId, request.PageNumber, request.PageSize, cancellationToken);

        var now = DateTime.UtcNow;
        var items = page.Items!.Select(s => s.ToSanctionDto(now)).ToList();

        logger.LogInformation(
            "Sanction history retrieved. User={UserId} Page={Page} Size={Size} Total={Total}",
            request.UserId, request.PageNumber, request.PageSize, page.TotalItems);

        return ResultT<PagedResult<SanctionDto>>.Success(
            new PagedResult<SanctionDto>(items, page.TotalItems, request.PageNumber, request.PageSize)
        );
    }
}
