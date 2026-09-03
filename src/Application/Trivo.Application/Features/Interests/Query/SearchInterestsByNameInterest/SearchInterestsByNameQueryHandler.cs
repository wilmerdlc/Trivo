using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Interests;

namespace Trivo.Application.Features.Interests.Query.SearchInterestsByNameInterest;

internal sealed class SearchInterestsByNameQueryHandler(
    ILogger<SearchInterestsByNameQueryHandler> logger,
    IInterestRepository interestRepository,
    ICacheService cache
) : IQueryHandler<SearchInterestsByNameQuery, IEnumerable<InterestWithIdDto>>
{
    public async Task<ResultT<IEnumerable<InterestWithIdDto>>> Handle(SearchInterestsByNameQuery request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            logger.LogWarning("The name entered for the interest search is empty or whitespace.");

            return ResultT<IEnumerable<InterestWithIdDto>>.Failure(
                Error.Failure("400", "The interest name cannot be empty."));
        }

        var cachedDtos = await cache.GetOrSetAsync(
            CacheKeys.InterestSearch(request.Name),
            async () =>
            {
                var interests = await interestRepository.SearchByNameAsync(request.Name, cancellationToken);

                return interests.ToInterestWithIdDtoList();
            },
            CacheProfiles.Cold with { Tags = [CacheKeys.InterestCatalogTag] },
            cancellationToken);

        var dtoList = cachedDtos?.ToList();
        if (dtoList is null || !dtoList.Any())
        {
            logger.LogInformation("No interests were found matching the text: '{Name}'.", request.Name);

            return ResultT<IEnumerable<InterestWithIdDto>>.Failure(
                Error.Failure("404", "No interests were found matching the entered name."));
        }

        logger.LogInformation("Found {Count} interests matching '{Name}'.", dtoList.Count, request.Name);

        return ResultT<IEnumerable<InterestWithIdDto>>.Success(dtoList);
    }
}