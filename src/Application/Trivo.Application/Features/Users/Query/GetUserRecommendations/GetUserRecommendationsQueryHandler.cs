using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Features.Matching;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Query.GetUserRecommendations;

internal sealed class GetUserRecommendationsQueryHandler(
    IUserRepository userRepository,
    IExpertRepository expertRepository,
    IRecruiterRepository recruiterRepository,
    IMatchRepository matchRepository,
    IEmbeddingService embeddingService,
    IAiNotifier aiNotifier,
    IUnitOfWork unitOfWork,
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
            logger.LogWarning("No user was found with ID {UserId}.", request.UserId);

            return ResultT<PagedResult<UserAiRecommendationDto>>.Failure(
                Error.Failure("404", "The user was not found.")
            );
        }

        // With neither skills nor interests, RestrictToBestStructuralMatches has nothing to work
        // with and recommendations fall back entirely to embedding distance on free-text prose —
        // which testing showed can rank an unrelated candidate above a genuinely relevant one.
        // Rather than return those silently, tell the user their profile needs more data first.
        var hasSkills = currentUser.UserSkills is { Count: > 0 };
        var hasInterests = currentUser.UserInterests is { Count: > 0 };
        if (!hasSkills && !hasInterests)
        {
            logger.LogWarning(
                "User {UserId} has no skills or interests registered; refusing to generate unreliable recommendations.",
                currentUser.Id
            );

            return ResultT<PagedResult<UserAiRecommendationDto>>.Failure(
                Error.Failure("400", "Your profile has no skills or interests registered. Add at least one to receive accurate recommendations.")
            );
        }

        var role = await userRepository.GetUserRoleAsync(currentUser.Id, cancellationToken);

        // Recommendations only make sense once the user has picked a side (Expert or
        // Recruiter) — without one, targetRole below would silently default to Recruiter and
        // hand back a directionless result instead of telling the user what's actually missing.
        if (role != Roles.Expert.ToString() && role != Roles.Recruiter.ToString())
        {
            logger.LogWarning(
                "User {UserId} has no Expert or Recruiter profile yet; refusing to generate recommendations.",
                currentUser.Id
            );

            return ResultT<PagedResult<UserAiRecommendationDto>>.Failure(
                Error.Failure("400", "Complete your expert or recruiter profile before requesting recommendations.")
            );
        }

        // Legacy-data fallback: a user created/edited before this feature shipped won't have an
        // embedding yet. Generate one on-demand rather than failing the whole request; the
        // UserProfileChangedEvent flow keeps it fresh from here on.
        var embedding = currentUser.ProfileEmbedding;
        if (embedding is null)
        {
            logger.LogInformation(
                "User {UserId} has no stored profile embedding yet; generating one on demand.",
                currentUser.Id
            );

            var vector = await embeddingService.GetEmbeddingAsync(
                UserProfileTextBuilder.Build(currentUser),
                cancellationToken
            );
            embedding = new Pgvector.Vector(vector);

            await userRepository.UpdateProfileEmbeddingAsync(
                currentUser.Id, embedding, UserProfileTextBuilder.Hash(currentUser), cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        var targetRole = role == Roles.Recruiter.ToString() ? Roles.Expert : Roles.Recruiter;

        const int topN = 9;

        // Fetch a wider pool than topN so there's enough of the distance curve to detect a real
        // relevance cutoff (see FilterByRelevanceGap) instead of a fixed distance threshold, which
        // doesn't generalize across different queries' own score distributions.
        var candidates = await userRepository.GetSimilarUsersAsync(
            currentUser.Id,
            embedding,
            targetRole,
            poolSize: topN * 3,
            cancellationToken
        );

        // Don't re-suggest someone the user already has a match record with (pending, accepted,
        // or rejected) — a repeat suggestion here is a UX bug, not a ranking decision.
        var matchedUserIds = await matchRepository.GetMatchedCounterpartUserIdsAsync(currentUser.Id, cancellationToken);
        if (matchedUserIds.Count > 0)
        {
            var excluded = matchedUserIds.ToHashSet();
            candidates = candidates.Where(c => !excluded.Contains(c.User.Id)).ToList();
        }

        var (structuralMatches, hasStructuralOverlap) = RestrictToBestStructuralMatches(candidates, currentUser);

        // The user has skills/interests registered (Stage 1 already ruled out having none at
        // all), but none of them match anyone currently reachable — same unreliable situation as
        // having no data, just discovered one step later, after we actually had a pool to check
        // against. Refuse rather than fall back to prose-only ranking silently.
        if (!hasStructuralOverlap)
        {
            logger.LogWarning(
                "User {UserId} has skills/interests registered, but none match any candidate in the current pool; refusing to generate unreliable recommendations.",
                currentUser.Id
            );

            return ResultT<PagedResult<UserAiRecommendationDto>>.Failure(
                Error.Failure("400", "None of your registered skills or interests match anyone available right now. Recommendations will improve as more users join or as you add more skills/interests.")
            );
        }

        var (acceptedIds, rejectedIds) = await matchRepository.GetMatchHistorySignalAsync(currentUser.Id, cancellationToken);
        var rankedMatches = ApplyMatchHistoryAdjustment(structuralMatches, acceptedIds, rejectedIds);

        var recommendedUsers = FilterByRelevanceGap(rankedMatches, topN);

        var totalItems = recommendedUsers.Count;
        var pageItems = recommendedUsers.Paginate(request.PageNumber, request.PageSize).ToList();

        if (role == Roles.Expert.ToString())
        {
            var recruiterIds = pageItems.Select(u => u.Id);
            var recruitersByUserId = (await recruiterRepository.GetDetailsByUserIdsAsync(recruiterIds, cancellationToken))
                .ToDictionary(r => r.UserId!.Value);

            // Preserve pageItems' similarity-ranked order — a dictionary lookup per item, not a
            // per-item DB round trip.
            var recruiterDtos = pageItems
                .Where(u => recruitersByUserId.ContainsKey(u.Id))
                .Select(u => MatchMapper.MapToRecruiterDto(u, recruitersByUserId[u.Id]))
                .ToList();

            return ResultT<PagedResult<UserAiRecommendationDto>>.Success(
                new PagedResult<UserAiRecommendationDto>(recruiterDtos, totalItems, request.PageNumber, request.PageSize)
            );
        }

        if (role == Roles.Recruiter.ToString())
        {
            var expertIds = pageItems.Select(u => u.Id);
            var expertsByUserId = (await expertRepository.GetDetailsByUserIdsAsync(expertIds, cancellationToken))
                .ToDictionary(e => e.UserId!.Value);

            var expertDtos = pageItems
                .Where(u => expertsByUserId.ContainsKey(u.Id))
                .Select(u => MatchMapper.MapToExpertDto(u, expertsByUserId[u.Id]))
                .ToList();

            return ResultT<PagedResult<UserAiRecommendationDto>>.Success(
                new PagedResult<UserAiRecommendationDto>(expertDtos, totalItems, request.PageNumber, request.PageSize)
            );
        }

        var userDtos = pageItems.Select(UserMapper.MapToAiRecommendationDto).ToList();

        var result = new PagedResult<UserAiRecommendationDto>(userDtos, totalItems, request.PageNumber, request.PageSize);

        await aiNotifier.NotifyRecommendationsAsync(currentUser.Id, userDtos);

        return ResultT<PagedResult<UserAiRecommendationDto>>.Success(result);
    }

    /// <summary>
    /// Restricts the candidate pool to whichever subset shares the most registered skills/interests
    /// with the querying user, before ranking by embedding distance — a defensive layer for when
    /// free-text prose (bio, position) could otherwise outweigh a real structural match, the way a
    /// generically-worded recruiter bio outranked a candidate with the exact skills it was looking
    /// for in testing. The second element reports whether any structural overlap was actually
    /// found at all — the caller uses that to refuse the request instead of silently falling back
    /// to a prose-only ranking that testing showed isn't reliable.
    /// </summary>
    private static (IReadOnlyList<(User User, double Distance)> Candidates, bool HasOverlap) RestrictToBestStructuralMatches(
        IReadOnlyList<(User User, double Distance)> candidates,
        User currentUser)
    {
        var currentSkillIds = currentUser.UserSkills?
            .Where(us => us.SkillId.HasValue)
            .Select(us => us.SkillId!.Value)
            .ToHashSet() ?? [];

        var currentInterestIds = currentUser.UserInterests?
            .Where(ui => ui.InterestId.HasValue)
            .Select(ui => ui.InterestId!.Value)
            .ToHashSet() ?? [];

        int OverlapScore(User candidate)
        {
            var skillOverlap = candidate.UserSkills?.Count(us => us.SkillId.HasValue && currentSkillIds.Contains(us.SkillId.Value)) ?? 0;
            var interestOverlap = candidate.UserInterests?.Count(ui => ui.InterestId.HasValue && currentInterestIds.Contains(ui.InterestId.Value)) ?? 0;
            return skillOverlap + interestOverlap;
        }

        var scored = candidates.Select(c => (c.User, c.Distance, Overlap: OverlapScore(c.User))).ToList();
        var maxOverlap = scored.Count > 0 ? scored.Max(c => c.Overlap) : 0;

        if (maxOverlap == 0)
        {
            return (candidates, false);
        }

        IReadOnlyList<(User User, double Distance)> restricted =
            scored.Where(c => c.Overlap == maxOverlap).Select(c => (c.User, c.Distance)).ToList();

        return (restricted, true);
    }

    /// <summary>
    /// Nudges each candidate's distance based on this user's own match history: closer if the
    /// candidate shares skills/interests with people whose match was <c>Completed</c>, further if
    /// they overlap with people who were <c>Rejected</c>. The nudge is capped at 5% of the pool's
    /// own distance spread per net accepted-vs-rejected overlap point, so it can re-order close
    /// candidates but can't override a large genuine semantic gap. Below 3 decided matches total
    /// there isn't enough signal to trust — that's noise, not a preference — so it's a no-op.
    /// </summary>
    private static IReadOnlyList<(User User, double Distance)> ApplyMatchHistoryAdjustment(
        IReadOnlyList<(User User, double Distance)> candidates,
        IReadOnlyList<Guid> acceptedIds,
        IReadOnlyList<Guid> rejectedIds)
    {
        if (acceptedIds.Count + rejectedIds.Count < 3 || candidates.Count == 0)
        {
            return candidates;
        }

        var accepted = acceptedIds.ToHashSet();
        var rejected = rejectedIds.ToHashSet();

        var range = candidates.Max(c => c.Distance) - candidates.Min(c => c.Distance);
        if (range <= 0)
        {
            return candidates;
        }

        const double weightPerNetOverlap = 0.05;

        return candidates
            .Select(c =>
            {
                var candidateIds = (c.User.UserSkills?.Where(us => us.SkillId.HasValue).Select(us => us.SkillId!.Value) ?? [])
                    .Concat(c.User.UserInterests?.Where(ui => ui.InterestId.HasValue).Select(ui => ui.InterestId!.Value) ?? []);

                var netOverlap = candidateIds.Count(id => accepted.Contains(id)) - candidateIds.Count(id => rejected.Contains(id));

                return (c.User, Distance: c.Distance - netOverlap * range * weightPerNetOverlap);
            })
            .OrderBy(c => c.Distance)
            .ToList();
    }

    /// <summary>
    /// Cuts the candidate list (already ordered by ascending distance) at the largest gap between
    /// consecutive distances, when that gap is clearly bigger than the typical gap between the
    /// other candidates — a genuine "relevant vs. not" break, not just noise. With fewer than 3
    /// candidates there's nothing to compare a gap against, so nothing gets cut. Always capped to
    /// topN.
    /// </summary>
    private static List<User> FilterByRelevanceGap(
        IReadOnlyList<(User User, double Distance)> candidates,
        int topN)
    {
        if (candidates.Count < 3)
        {
            return candidates.Take(topN).Select(c => c.User).ToList();
        }

        var gaps = new double[candidates.Count - 1];
        for (var i = 1; i < candidates.Count; i++)
        {
            gaps[i - 1] = candidates[i].Distance - candidates[i - 1].Distance;
        }

        var maxGapIndex = 0;
        for (var i = 1; i < gaps.Length; i++)
        {
            if (gaps[i] > gaps[maxGapIndex])
            {
                maxGapIndex = i;
            }
        }

        var otherGapsAverage = gaps.Where((_, i) => i != maxGapIndex).DefaultIfEmpty(0).Average();

        var cutCount = otherGapsAverage > 0 && gaps[maxGapIndex] > otherGapsAverage * 1.5
            ? maxGapIndex + 1
            : candidates.Count;

        return candidates.Take(Math.Min(cutCount, topN)).Select(c => c.User).ToList();
    }
}
