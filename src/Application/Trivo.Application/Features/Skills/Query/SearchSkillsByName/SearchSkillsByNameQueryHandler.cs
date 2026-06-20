using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.DTOs.Skills;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Utils;

namespace Trivo.Application.Features.Skills.Query.SearchSkillsByName;

internal sealed class SearchSkillsByNameQueryHandler(
    ILogger<SearchSkillsByNameQueryHandler> logger,
    ISkillRepository skillRepository,
    IDistributedCache cache
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

        var cacheKey = $"search-skills-by-name-{request.Name.Trim().ToLowerInvariant()}";

        var skills = await cache.GetOrCreateAsync(
            cacheKey,
            async () => await skillRepository.SearchByNameAsync(request.Name, cancellationToken),
            cancellationToken: cancellationToken
        );

        if (!skills.Any())
        {
            logger.LogInformation("No skills found matching the name: '{Name}'.", request.Name);

            return ResultT<IEnumerable<SkillWithIdDto>>.Failure(
                Error.Failure("404", "No skills were found matching the provided name."));
        }

        var skillDtos = skills.Select(x => new SkillWithIdDto(
            SkillId: x.SkillId ?? Guid.Empty,
            Name: x.Name ?? string.Empty
        )).ToList();

        logger.LogInformation("Found {Count} skill(s) matching '{Name}'.", skillDtos.Count, request.Name);

        return ResultT<IEnumerable<SkillWithIdDto>>.Success(skillDtos);
    }
}