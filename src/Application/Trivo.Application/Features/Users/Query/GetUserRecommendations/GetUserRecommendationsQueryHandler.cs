using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Features.Matching;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Query.GetUserRecommendations;

internal sealed partial class GetUserRecommendationsQueryHandler(
    IUserRepository userRepository,
    IExpertRepository expertRepository,
    IRecruiterRepository recruiterRepository,
    IAiCompletionService aiCompletionService,
    IAiNotifier aiNotifier,
    IDistributedCache cache,
    ILogger<GetUserRecommendationsQueryHandler> logger
) : IQueryHandler<GetUserRecommendationsQuery, PagedResult<UserAiRecommendationDto>>
{
    public async Task<ResultT<PagedResult<UserAiRecommendationDto>>> Handle(
        GetUserRecommendationsQuery request,
        CancellationToken cancellationToken)
    {
        var currentUser = await userRepository.GetUserWithInterestsAndSkillsAsync(request.UserId, cancellationToken);
        if (currentUser is null)
        {
            logger.LogWarning(
                "No user was found with ID {UserId}, or they have no registered interests/skills.",
                request.UserId
            );

            return ResultT<PagedResult<UserAiRecommendationDto>>.Failure(
                Error.Failure("404", "The user was not found or has no interest/skill data.")
            );
        }

        var role = await userRepository.GetUserRoleAsync(currentUser.Id, cancellationToken);

        var targetUsers = (await userRepository.GetTargetUsersAsync(currentUser.Id, role, cancellationToken)).ToList();

        var messages = UserRecommendationPromptBuilder.Build(currentUser, targetUsers, role);

        logger.LogInformation(
            "Prompt built successfully for user {UserId}. Sending request to the AI...",
            currentUser.Id
        );

        // The AI is asked to pick candidates from `targetUsers` based on the prompt. If it fails to
        // respond, or doesn't return usable IDs, we fall back to a DB-driven similarity ranking, and
        // ultimately to the full candidate pool, so the client always gets a result.
        var aiResponse = await cache.GetOrCreateAsync(
            $"ai-recommendation-response-{currentUser.Id}",
            async () => await aiCompletionService.GetCompletionAsync(messages, cancellationToken),
            cancellationToken: cancellationToken
        );

        var recommendedUsers = new List<User>();

        if (!string.IsNullOrWhiteSpace(aiResponse))
        {
            var recommendedIds = GuidPattern().Matches(aiResponse)
                .Select(m => Guid.TryParse(m.Value, out var guid) ? guid : Guid.Empty)
                .Where(g => g != Guid.Empty)
                .Distinct()
                .ToList();

            recommendedUsers = targetUsers.Where(u => recommendedIds.Contains(u.Id)).ToList();
        }
        else
        {
            logger.LogWarning("The AI returned an empty or null response for user {UserId}.", currentUser.Id);
        }

        if (recommendedUsers.Count == 0)
        {
            logger.LogWarning("The AI did not return valid IDs. Falling back to interest/skill similarity ranking.");

            recommendedUsers = GetSimilarUsers(currentUser, targetUsers);

            if (recommendedUsers.Count == 0)
            {
                logger.LogInformation("No similar users were found either; falling back to the full candidate pool.");

                recommendedUsers = targetUsers;
            }
        }

        var totalItems = recommendedUsers.Count;
        var pageItems = recommendedUsers.Paginate(request.PageNumber, request.PageSize).ToList();

        if (role == Roles.Expert.ToString())
        {
            var recruiterDtos = new List<UserAiRecommendationDto>();

            foreach (var user in pageItems)
            {
                var recruiter = await recruiterRepository.GetDetailsAsync(user.Id, cancellationToken);
                if (recruiter is not null)
                {
                    recruiterDtos.Add(MatchMapper.MapToRecruiterDto(user, recruiter));
                }
            }

            return ResultT<PagedResult<UserAiRecommendationDto>>.Success(
                new PagedResult<UserAiRecommendationDto>(recruiterDtos, totalItems, request.PageNumber, request.PageSize)
            );
        }

        if (role == Roles.Recruiter.ToString())
        {
            var expertDtos = new List<UserAiRecommendationDto>();

            foreach (var user in pageItems)
            {
                var expert = await expertRepository.GetDetailsAsync(user.Id, cancellationToken);
                if (expert is not null)
                {
                    expertDtos.Add(MatchMapper.MapToExpertDto(user, expert));
                }
            }

            return ResultT<PagedResult<UserAiRecommendationDto>>.Success(
                new PagedResult<UserAiRecommendationDto>(expertDtos, totalItems, request.PageNumber, request.PageSize)
            );
        }

        var userDtos = pageItems.Select(UserMapper.MapToAiRecommendationDto).ToList();

        var result = new PagedResult<UserAiRecommendationDto>(userDtos, totalItems, request.PageNumber, request.PageSize);

        await aiNotifier.NotifyRecommendationsAsync(currentUser.Id, userDtos);

        return ResultT<PagedResult<UserAiRecommendationDto>>.Success(result);
    }

    [GeneratedRegex(@"[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}")]
    private static partial Regex GuidPattern();

    private static List<User> GetSimilarUsers(User currentUser, List<User> users, int topN = 9)
    {
        int CalculateSimilarity(User u)
        {
            var sharedInterests = u.UserInterests?.Count(i =>
                currentUser.UserInterests?.Any(ci => ci.InterestId == i.InterestId) ?? false) ?? 0;

            var sharedSkills = u.UserSkills?.Count(s =>
                currentUser.UserSkills?.Any(cs => cs.SkillId == s.SkillId) ?? false) ?? 0;

            return sharedInterests + sharedSkills;
        }

        return users
            .Select(u => new { User = u, Similarity = CalculateSimilarity(u) })
            .Where(x => x.Similarity > 0)
            .OrderByDescending(x => x.Similarity)
            .ThenBy(x => x.User.FirstName)
            .Take(topN)
            .Select(x => x.User)
            .ToList();
    }

}
