using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Features.InterestCategories;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.InterestCategories;

namespace Trivo.Application.Features.InterestCategories.Query.GetPaginatedInterestCategories;

internal sealed class GetPaginatedInterestCategoriesQueryHandler(
    ILogger<GetPaginatedInterestCategoriesQueryHandler> logger,
    IDistributedCache cache,
    IInterestCategoryRepository repository
) : IQueryHandler<GetPaginatedInterestCategoriesQuery, PagedResult<InterestCategoryDto>>
{
    public async Task<ResultT<PagedResult<InterestCategoryDto>>> Handle(GetPaginatedInterestCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            logger.LogWarning("The request to get paginated interest categories is null.");
            return ResultT<PagedResult<InterestCategoryDto>>.Failure(
                Error.Failure("400", "The request cannot be null."));
        }

        if (!PaginationValidator.TryValidate(request.PageNumber, request.PageSize, logger,
                out ResultT<PagedResult<InterestCategoryDto>> validationFailure))
        {
            return validationFailure;
        }

        string cacheKey = $"get-paginated-interest-categories-{request.PageNumber}-{request.PageSize}";

        var pagedEntities = await cache.GetOrCreateAsync(cacheKey, async () =>
        {
            var paged = await repository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);

            var dtoList = paged.Items?.Select(entity => entity.ToDto()).ToList() ?? [];

            return new PagedResult<InterestCategoryDto>(
                items: dtoList,
                totalItems: paged.TotalItems,
                currentPage: request.PageNumber,
                pageSize: request.PageSize
            );
        }, cancellationToken: cancellationToken);

        logger.LogInformation("Successfully retrieved {Count} interest categories for page {PageNumber}.",
            pagedEntities.Items!.Count(), request.PageNumber);

        return ResultT<PagedResult<InterestCategoryDto>>.Success(pagedEntities);
    }
}