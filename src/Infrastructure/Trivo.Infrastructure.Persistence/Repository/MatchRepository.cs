using Microsoft.EntityFrameworkCore;
using Trivo.Application.Interfaces.Repository;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;
using Trivo.Infrastructure.Persistence.Base;
using Trivo.Infrastructure.Persistence.Context;

namespace Trivo.Infrastructure.Persistence.Repository;

public class MatchRepository(TrivoContext context) : GenericRepository<Match>(context), IMatchRepository
{
    public async Task<Match?> GetAsync(Guid expertId, Guid recruiterId, CancellationToken cancellationToken)
    {
        return await Context.Set<Match>()
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ExpertId == expertId && m.RecruiterId == recruiterId, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid expertId, Guid recruiterId, CancellationToken cancellationToken)
    {
        return await ValidateAsync(x => x.ExpertId == expertId && x.RecruiterId == recruiterId, cancellationToken);
    }

    // Only Recruiter.User's skills/interests are ever read by the caller (GetMatchByUserQueryHandler
    // maps just RecruiterDto here) — Expert stays a bare Include for its scalar Id, no need to
    // pull its User/Skills/Interests graph too.
    public async Task<IEnumerable<Match>> GetAsExpertAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await Context.Set<Match>()
            .AsNoTracking()
            .Include(m => m.Expert)
            .Include(m => m.Recruiter)
            .ThenInclude(r => r!.User)
            .ThenInclude(u => u!.UserSkills)!
            .ThenInclude(us => us.Skill)
            .Include(m => m.Recruiter)
            .ThenInclude(r => r!.User)
            .ThenInclude(u => u!.UserInterests)!
            .ThenInclude(ui => ui.Interest)
            .AsSplitQuery()
            .Where(m => m.Expert!.UserId == userId)
            .Where(m => m.MatchStatus == MatchStatus.Pending.ToString())
            .ToListAsync(cancellationToken);
    }

    // Mirror of GetAsExpertAsync: only Expert.User's skills/interests are read here (ExpertDto).
    public async Task<IEnumerable<Match>> GetAsRecruiterAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await Context.Set<Match>()
            .AsNoTracking()
            .Include(m => m.Recruiter)
            .Include(m => m.Expert)
            .ThenInclude(ex => ex!.User)
            .ThenInclude(u => u!.UserSkills)!
            .ThenInclude(us => us.Skill)
            .Include(m => m.Expert)
            .ThenInclude(ex => ex!.User)
            .ThenInclude(u => u!.UserInterests)!
            .ThenInclude(ui => ui.Interest)
            .AsSplitQuery()
            .Where(m => m.Recruiter!.UserId == userId)
            .Where(m => m.MatchStatus == MatchStatus.Pending.ToString())
            .ToListAsync(cancellationToken);
    }

    public async Task<Match?> GetDetailsByIdAsync(Guid matchId, CancellationToken cancellationToken)
    {
        return await Context.Set<Match>()
            .AsNoTracking()
            .Include(m => m.Expert)
            .ThenInclude(e => e!.User)
            .ThenInclude(u => u!.UserSkills)!
            .ThenInclude(us => us.Skill)
            .Include(m => m.Expert)
            .ThenInclude(e => e!.User)
            .ThenInclude(u => u!.UserInterests)!
            .ThenInclude(ui => ui.Interest)
            .Include(m => m.Recruiter)
            .ThenInclude(r => r!.User)
            .ThenInclude(u => u!.UserSkills)!
            .ThenInclude(us => us.Skill)
            .Include(m => m.Recruiter)
            .ThenInclude(r => r!.User)
            .ThenInclude(u => u!.UserInterests)!
            .ThenInclude(ui => ui.Interest)
            .AsSplitQuery()
            .FirstOrDefaultAsync(m => m.Id == matchId, cancellationToken);
    }

    public async Task<IReadOnlyList<Guid>> GetMatchedCounterpartUserIdsAsync(Guid userId, CancellationToken cancellationToken)
    {
        var asExpertCounterparts = Context.Set<Match>()
            .AsNoTracking()
            .Where(m => m.Expert!.UserId == userId && m.Recruiter!.UserId != null)
            .Select(m => m.Recruiter!.UserId!.Value);

        var asRecruiterCounterparts = Context.Set<Match>()
            .AsNoTracking()
            .Where(m => m.Recruiter!.UserId == userId && m.Expert!.UserId != null)
            .Select(m => m.Expert!.UserId!.Value);

        return await asExpertCounterparts.Union(asRecruiterCounterparts).ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<Guid> AcceptedIds, IReadOnlyList<Guid> RejectedIds)> GetMatchHistorySignalAsync(
        Guid userId, CancellationToken cancellationToken)
    {
        var decidedCounterparts = await Context.Set<Match>()
            .AsNoTracking()
            .Where(m => (m.Expert!.UserId == userId || m.Recruiter!.UserId == userId) &&
                        (m.MatchStatus == MatchStatus.Completed.ToString() || m.MatchStatus == MatchStatus.Rejected.ToString()))
            .Select(m => new
            {
                m.MatchStatus,
                CounterpartUserId = m.Expert!.UserId == userId ? m.Recruiter!.UserId : m.Expert!.UserId
            })
            .Where(x => x.CounterpartUserId != null)
            .ToListAsync(cancellationToken);

        var acceptedUserIds = decidedCounterparts
            .Where(x => x.MatchStatus == MatchStatus.Completed.ToString())
            .Select(x => x.CounterpartUserId!.Value)
            .ToList();

        var rejectedUserIds = decidedCounterparts
            .Where(x => x.MatchStatus == MatchStatus.Rejected.ToString())
            .Select(x => x.CounterpartUserId!.Value)
            .ToList();

        var acceptedIds = await GetSkillAndInterestIdsAsync(acceptedUserIds, cancellationToken);
        var rejectedIds = await GetSkillAndInterestIdsAsync(rejectedUserIds, cancellationToken);

        return (acceptedIds, rejectedIds);
    }

    // Flat joins on the link tables — same shape as UserRepository.GetSkillsAsync/GetInterestsAsync
    // — instead of projecting two navigation collections combined with Concat() inside one Select,
    // which EF Core's query translator rejects ("Unable to translate a collection subquery...").
    private async Task<IReadOnlyList<Guid>> GetSkillAndInterestIdsAsync(IReadOnlyList<Guid> userIds, CancellationToken cancellationToken)
    {
        if (userIds.Count == 0)
        {
            return [];
        }

        var skillIds = await Context.Set<UserSkill>()
            .AsNoTracking()
            .Where(us => us.UserId.HasValue && userIds.Contains(us.UserId.Value) && us.SkillId.HasValue)
            .Select(us => us.SkillId!.Value)
            .ToListAsync(cancellationToken);

        var interestIds = await Context.Set<UserInterest>()
            .AsNoTracking()
            .Where(ui => ui.UserId.HasValue && userIds.Contains(ui.UserId.Value) && ui.InterestId.HasValue)
            .Select(ui => ui.InterestId!.Value)
            .ToListAsync(cancellationToken);

        return skillIds.Concat(interestIds).Distinct().ToList();
    }

    public async Task UpdateStatusAsync(Guid matchId,
        MatchUpdateStatus? status, CancellationToken cancellationToken)
    {
        var match = await Context.Set<Match>()
            .FirstOrDefaultAsync(m => m.Id == matchId, cancellationToken);

        if (match != null)
        {
            match.MatchStatus = status.ToString()!;
            match.ExpertStatus = status.ToString()!;
            match.RecruiterStatus = status.ToString()!;
            match.UpdatedAt = DateTime.UtcNow;

            Context.Update(match);
        }

        await Task.CompletedTask;
    }
}