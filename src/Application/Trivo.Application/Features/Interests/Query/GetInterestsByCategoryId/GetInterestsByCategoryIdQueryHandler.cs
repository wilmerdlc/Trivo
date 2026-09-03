using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Interests;

namespace Trivo.Application.Features.Interests.Query.GetInterestsByCategoryId;

internal sealed class GetInterestsByCategoryIdQueryHandler(
    ILogger<GetInterestsByCategoryIdQueryHandler> logger,
    IInterestRepository interestRepository,
    ICacheService cache
) : IQueryHandler<GetInterestsByCategoryIdQuery, PagedResult<InterestByCategoryIdDto>>
{
    public async Task<ResultT<PagedResult<InterestByCategoryIdDto>>> Handle(GetInterestsByCategoryIdQuery request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            logger.LogWarning("The request to get interests by category was null.");

            return ResultT<PagedResult<InterestByCategoryIdDto>>.Failure(
                Error.Failure("400", "The request cannot be null."));
        }

        if (request.PageNumber <= 0 || request.PageSize <= 0)
        {
            logger.LogWarning("Invalid pagination parameters. PageNumber: {PageNumber}, PageSize: {PageSize}",
                request.PageNumber, request.PageSize);

            return ResultT<PagedResult<InterestByCategoryIdDto>>.Failure(
                Error.Conflict("409", "Pagination parameters must be greater than zero."));
        }

        if (!request.CategoryIds.Any())
        {
            logger.LogWarning("No categories were provided to retrieve interests.");

            return ResultT<PagedResult<InterestByCategoryIdDto>>.Failure(
                Error.Failure("400", "At least one category must be provided."));
        }

        var pagedResponse = await cache.GetOrSetAsync(
            CacheKeys.InterestsByCategory(request.CategoryIds, request.PageNumber, request.PageSize),
            async () =>
            {
                var paged = await interestRepository.GetPagedByCategoriesAsync(request.CategoryIds,
                    request.PageNumber,
                    request.PageSize,
                    cancellationToken);

                IEnumerable<InterestByCategoryIdDto> dtoList = paged.Items!.ToInterestByCategoryIdDtoList();

                var pagedResponse = new PagedResult<InterestByCategoryIdDto>(
                    items: dtoList,
                    totalItems: paged.TotalItems,
                    currentPage: request.PageNumber,
                    pageSize: request.PageSize
                );

                return pagedResponse;
            },
            CacheProfiles.Cold with { Tags = [CacheKeys.InterestCatalogTag] },
            cancellationToken);


        logger.LogInformation("Interests by category successfully retrieved. Categories: {CategoryIds}, Total: {Total}",
            string.Join(", ", request.CategoryIds), pagedResponse.TotalItems);

        return ResultT<PagedResult<InterestByCategoryIdDto>>.Success(pagedResponse);
    }
}