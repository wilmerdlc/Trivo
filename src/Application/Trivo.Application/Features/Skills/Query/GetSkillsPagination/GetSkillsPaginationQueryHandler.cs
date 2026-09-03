using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Skills;

namespace Trivo.Application.Features.Skills.Query.GetSkillsPagination;

internal sealed class GetSkillsPaginationQueryHandler(
    ILogger<GetSkillsPaginationQueryHandler> logger,
    ISkillRepository skillRepository,
    ICacheService cache
) : IQueryHandler<GetSkillsPaginationQuery, PagedResult<SkillDto>>
{
    public async Task<ResultT<PagedResult<SkillDto>>> Handle(GetSkillsPaginationQuery request, CancellationToken cancellationToken)
    {
        if (request.PageNumber <= 0 || request.PageSize <= 0)
        {
            logger.LogWarning("Invalid pagination parameters: PageNumber={PageNumber}, PageSize={PageSize}",
                request.PageNumber, request.PageSize);

            return ResultT<PagedResult<SkillDto>>.Failure(
                Error.Failure("400", "Pagination parameters must be greater than zero."));
        }

        var pagedResult = await cache.GetOrSetAsync(
            CacheKeys.SkillsPaged(request.PageNumber, request.PageSize),
            async () =>
            {
                var paginationResult = await skillRepository.GetPagedAsync(
                    request.PageNumber,
                    request.PageSize,
                    cancellationToken);

                var items = paginationResult.Items.ToSkillDtoList();

                return new PagedResult<SkillDto>(
                    items: items,
                    totalItems: paginationResult.TotalItems,
                    currentPage: request.PageNumber,
                    pageSize: request.PageSize
                );
            },
            CacheProfiles.Cold with { Tags = [CacheKeys.SkillCatalogTag] },
            cancellationToken
        );

        logger.LogInformation(
            "Paginated skills retrieved successfully. Page {PageNumber}, Size {PageSize}, TotalItems={Total}",
            request.PageNumber, request.PageSize, pagedResult.TotalItems);

        return ResultT<PagedResult<SkillDto>>.Success(pagedResult);
    }
}