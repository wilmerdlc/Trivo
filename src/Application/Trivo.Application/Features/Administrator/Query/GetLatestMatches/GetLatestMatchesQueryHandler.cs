using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Features.Administrator;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Query.GetLatestMatches;

internal sealed class GetLatestMatchesQueryHandler(
    ILogger<GetLatestMatchesQueryHandler> logger,
    IDistributedCache cache,
    IAdministratorRepository administratorRepository
) : IQueryHandler<GetLatestMatchesQuery, PagedResult<AdminMatchDto>>
{
    public async Task<ResultT<PagedResult<AdminMatchDto>>> Handle(GetLatestMatchesQuery request,
        CancellationToken cancellationToken)
    {
        if (request.PageNumber <= 0 || request.PageSize <= 0)
        {
            logger.LogWarning(
                "Invalid pagination parameters: PageNumber ({PageNumber}) or PageSize ({PageSize}) must be greater than zero.",
                request.PageNumber,
                request.PageSize
            );

            return ResultT<PagedResult<AdminMatchDto>>.Failure(
                Error.Failure("400", "Invalid pagination parameters.")
            );
        }

        var pagedMatches = await administratorRepository.GetPagedLatestMatchesAsync(
            request.PageNumber,
            request.PageSize,
            cancellationToken
        );

        if (!pagedMatches.Items!.Any())
        {
            logger.LogInformation(
                "No matches found for page {PageNumber} with size {PageSize}.",
                request.PageNumber,
                request.PageSize
            );

            return ResultT<PagedResult<AdminMatchDto>>.Success(
                new PagedResult<AdminMatchDto>(
                    Enumerable.Empty<AdminMatchDto>(),
                    0,
                    request.PageNumber,
                    request.PageSize
                )
            );
        }

        var cacheKey = $"latest-matches-{request.PageNumber}-{request.PageSize}";

        var result = await cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var dtoList = pagedMatches.Items!.Select(x => x.ToAdminDto()).ToList();

                return new PagedResult<AdminMatchDto>(
                    dtoList,
                    pagedMatches.TotalItems,
                    request.PageNumber,
                    request.PageSize
                );
            },
            cancellationToken: cancellationToken
        );

        logger.LogInformation(
            "Latest matches retrieved successfully for page {PageNumber} with size {PageSize}.",
            request.PageNumber,
            request.PageSize
        );

        return ResultT<PagedResult<AdminMatchDto>>.Success(result);
    }
}