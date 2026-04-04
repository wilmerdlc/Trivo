using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.DTOs.Interests;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

namespace Trivo.Application.Features.Interests.Query.GetInterestsPagination;

internal sealed class GetInterestsPaginationQueryHandler(
    ILogger<GetInterestsPaginationQueryHandler> logger,
    IInterestRepository interestRepository,
    IDistributedCache cache
) : IQueryHandler<GetInterestsPaginationQuery, PagedResult<InterestDto>>
{
    public async Task<ResultT<PagedResult<InterestDto>>> Handle(GetInterestsPaginationQuery request,
        CancellationToken cancellationToken)
    {
        if (request.PageNumber <= 0 || request.PageSize <= 0)
        {
            logger.LogWarning(
                "GetInterestsPaginationQuery received invalid parameters: PageNumber={PageNumber}, PageSize={PageSize}",
                request.PageNumber, request.PageSize);

            return ResultT<PagedResult<InterestDto>>.Failure(
                Error.Failure("400", "Pagination parameters must be greater than zero."));
        }

        var pagedResult = await cache.GetOrCreateAsync(
            $"paginated-interests-{request.PageNumber}-{request.PageSize}",
            async () =>
            {
                var paginationResult = await interestRepository.GetPagedAsync(
                    request.PageNumber,
                    request.PageSize,
                    cancellationToken);

                var paginatedDtoList = paginationResult.Items.ToInterestDtoList();

                var total = paginationResult.TotalItems;

                return new PagedResult<InterestDto>(
                    items: paginatedDtoList,
                    totalItems: total,
                    currentPage: request.PageNumber,
                    pageSize: request.PageSize
                );
            },
            cancellationToken: cancellationToken
        );

        logger.LogInformation(
            "Paginated interests retrieved successfully. Page {PageNumber}, Size {PageSize}, TotalItems={Total}",
            request.PageNumber, request.PageSize, pagedResult.TotalItems);

        return ResultT<PagedResult<InterestDto>>.Success(pagedResult);
    }
}