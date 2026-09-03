using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Features.Matching;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Query.GetUsersByInterestsAndSkills;

internal sealed class GetUsersByInterestsAndSkillsQueryHandler(
    ILogger<GetUsersByInterestsAndSkillsQueryHandler> logger,
    IUserRepository userRepository
) : IQueryHandler<GetUsersByInterestsAndSkillsQuery, PagedResult<UserAiRecommendationDto>>
{
    public async Task<ResultT<PagedResult<UserAiRecommendationDto>>> Handle(
        GetUsersByInterestsAndSkillsQuery request,
        CancellationToken cancellationToken)
    {
        if (request.PageNumber <= 0 || request.PageSize <= 0)
        {
            logger.LogWarning(
                "Invalid pagination parameters: PageSize={PageSize}, PageNumber={PageNumber}",
                request.PageSize,
                request.PageNumber
            );

            return ResultT<PagedResult<UserAiRecommendationDto>>.Failure(
                Error.Failure("400", "Page number and page size must be greater than zero.")
            );
        }

        var users = (await userRepository.GetByInterestsAndSkillsAsync(
            request.InterestIds, request.SkillIds, cancellationToken))
            .Where(u => u.Id != request.RequesterId)
            .ToList();

        if (users.Count == 0)
        {
            logger.LogWarning("No users were found matching the provided interests and skills.");

            return ResultT<PagedResult<UserAiRecommendationDto>>.Failure(
                Error.NotFound("404", "No users were found matching the specified interests and skills.")
            );
        }

        // Not cached, deliberately — this is a candidate-discovery endpoint. A stale entry here
        // could keep showing a banned or already-matched user, or hide someone whose availability
        // just changed; that's a correctness risk this app can't accept for matchmaking results.
        var pageUsers = users.Paginate(request.PageNumber, request.PageSize).ToList();
        var pageDtos = pageUsers.Select(MapUser).ToList();
        var result = new PagedResult<UserAiRecommendationDto>(pageDtos, users.Count, request.PageNumber, request.PageSize);

        logger.LogInformation(
            "Users filtered successfully by interests and skills. Total found: {TotalUsers}",
            result.TotalItems
        );

        return ResultT<PagedResult<UserAiRecommendationDto>>.Success(result);
    }

    private static UserAiRecommendationDto MapUser(User user)
    {
        if (user.Recruiters is { Count: > 0 })
        {
            return MatchMapper.MapToRecruiterDto(user, user.Recruiters.First());
        }

        if (user.Experts is { Count: > 0 })
        {
            return MatchMapper.MapToExpertDto(user, user.Experts.First());
        }

        return UserMapper.MapToAiRecommendationDto(user);
    }
}
