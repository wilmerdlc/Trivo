using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Features.Matching;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Query.SearchUsers;

internal sealed class SearchUsersQueryHandler(
    IUserRepository userRepository,
    IMatchRepository matchRepository,
    ILogger<SearchUsersQueryHandler> logger
) : IQueryHandler<SearchUsersQuery, PagedResult<UserAiRecommendationDto>>
{
    private const int MaxTerms = 5;

    public async Task<ResultT<PagedResult<UserAiRecommendationDto>>> Handle(
        SearchUsersQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("No user was found with ID '{UserId}'.", request.UserId);

            return ResultT<PagedResult<UserAiRecommendationDto>>.Failure(
                Error.NotFound("404", "The user was not found.")
            );
        }

        var role = await userRepository.GetUserRoleAsync(user.Id, cancellationToken);

        // Experts only see Recruiters and vice versa, so a user without either profile has no
        // "other side" to search — same rule as the recommendations endpoint.
        if (role != Roles.Expert.ToString() && role != Roles.Recruiter.ToString())
        {
            logger.LogWarning("User {UserId} has no Expert or Recruiter profile yet; refusing to search.", user.Id);

            return ResultT<PagedResult<UserAiRecommendationDto>>.Failure(
                Error.Validation("400", "Complete your expert or recruiter profile before searching.")
            );
        }

        var terms = request.Text
            .Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries)
            .Select(t => new string(t.Where(c => c is not ('%' or '_' or '\\')).ToArray()))
            .Where(t => t.Length > 0)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Take(MaxTerms)
            .ToList();

        if (terms.Count == 0)
        {
            return ResultT<PagedResult<UserAiRecommendationDto>>.Failure(
                Error.Validation("400", "The search text is required.")
            );
        }

        var targetRole = role == Roles.Recruiter.ToString() ? Roles.Expert : Roles.Recruiter;

        var matchedUserIds = await matchRepository.GetMatchedCounterpartUserIdsAsync(user.Id, cancellationToken);

        var (users, total) = await userRepository.SearchByTextAsync(
            user.Id,
            targetRole,
            terms,
            matchedUserIds,
            request.PageNumber,
            request.PageSize,
            cancellationToken
        );

        // Not cached, deliberately — same reasoning as the other candidate-discovery endpoints:
        // a stale entry could keep showing a banned or already-matched user.
        var items = users.Select(MapUser).ToList();

        logger.LogInformation(
            "User {UserId} ({Role}) searched for '{Text}'. Total found: {Total}",
            user.Id,
            role,
            request.Text,
            total
        );

        return ResultT<PagedResult<UserAiRecommendationDto>>.Success(
            new PagedResult<UserAiRecommendationDto>(items, total, request.PageNumber, request.PageSize)
        );
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
