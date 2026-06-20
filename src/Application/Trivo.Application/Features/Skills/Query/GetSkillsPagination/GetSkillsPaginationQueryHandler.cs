using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.DTOs.Skills;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

namespace Trivo.Application.Features.Skills.Query.GetSkillsPagination;

internal sealed class GetSkillsPaginationQueryHandler(
    ILogger<GetSkillsPaginationQueryHandler> logger,
    ISkillRepository skillRepository,
    IDistributedCache cache
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

        var pagedResult = await cache.GetOrCreateAsync(
            $"paginated-skills-{request.PageNumber}-{request.PageSize}",
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
            cancellationToken: cancellationToken
        );

        logger.LogInformation(
            "Paginated skills retrieved successfully. Page {PageNumber}, Size {PageSize}, TotalItems={Total}",
            request.PageNumber, request.PageSize, pagedResult.TotalItems);

        return ResultT<PagedResult<SkillDto>>.Success(pagedResult);
    }
}