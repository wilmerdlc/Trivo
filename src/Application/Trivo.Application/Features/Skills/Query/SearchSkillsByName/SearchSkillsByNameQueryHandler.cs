using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Skills;

namespace Trivo.Application.Features.Skills.Query.SearchSkillsByName;

internal sealed class SearchSkillsByNameQueryHandler(
    ILogger<SearchSkillsByNameQueryHandler> logger,
    ISkillRepository skillRepository,
    ICacheService cache
) : IQueryHandler<SearchSkillsByNameQuery, IEnumerable<SkillWithIdDto>>
{
    public async Task<ResultT<IEnumerable<SkillWithIdDto>>> Handle(SearchSkillsByNameQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            logger.LogWarning("The skill name provided for the search is empty or whitespace.");

            return ResultT<IEnumerable<SkillWithIdDto>>.Failure(
                Error.Failure("400", "The skill name cannot be empty."));
        }

        var skills = await cache.GetOrSetAsync(
            CacheKeys.SkillSearch(request.Name),
            async () => await skillRepository.SearchByNameAsync(request.Name, cancellationToken),
            CacheProfiles.Cold with { Tags = [CacheKeys.SkillCatalogTag] },
            cancellationToken
        );

        if (!skills.Any())
        {
            logger.LogInformation("No skills found matching the name: '{Name}'.", request.Name);

            return ResultT<IEnumerable<SkillWithIdDto>>.Failure(
                Error.NotFound("404", "No skills were found matching the provided name."));
        }

        var skillDtos = skills.Select(x => new SkillWithIdDto(
            SkillId: x.SkillId ?? Guid.Empty,
            Name: x.Name ?? string.Empty
        )).ToList();

        logger.LogInformation("Found {Count} skill(s) matching '{Name}'.", skillDtos.Count, request.Name);

        return ResultT<IEnumerable<SkillWithIdDto>>.Success(skillDtos);
    }
}