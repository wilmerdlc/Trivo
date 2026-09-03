using Microsoft.EntityFrameworkCore;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Domain.Models;
using Trivo.Infrastructure.Persistence.Base;
using Trivo.Infrastructure.Persistence.Context;

namespace Trivo.Infrastructure.Persistence.Repository.Account;

public class RecruiterRepository(TrivoContext context) : GenericRepository<Recruiter>(context), IRecruiterRepository
{
    public async Task<IEnumerable<Recruiter>> GetBySkillsAndInterestsAsync(
        List<Guid> skillIds,
        List<Guid> interestIds,
        CancellationToken cancellationToken)
    {
        return await Context.Set<Recruiter>()
            .AsNoTracking()
            .Include(r => r.User)
            .ThenInclude(u => u!.UserSkills)
            .Include(r => r.User)
            .ThenInclude(u => u!.UserInterests)
            .AsSplitQuery()
            .Where(r =>
                r.User != null &&
                (skillIds.Count == 0 ||
                 r.User.UserSkills!
                     .Any(us => skillIds.Contains((Guid)us.SkillId!))) &&
                (interestIds.Count == 0 ||
                 r.User.UserInterests!
                     .Any(ui => interestIds.Contains((Guid)ui.InterestId!))))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsUserRecruiterAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await ValidateAsync(r => r.UserId == userId, cancellationToken);
    }

    public async Task<Recruiter?> GetDetailsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await Context.Set<Recruiter>()
            .AsNoTracking()
            .Include(r => r.User)
            .ThenInclude(u => u!.UserSkills)!
            .ThenInclude(us => us.Skill)
            .Include(r => r.User)
            .ThenInclude(u => u!.UserInterests)!
            .ThenInclude(ui => ui.Interest)
            .FirstOrDefaultAsync(r => r.UserId == userId, cancellationToken);
    }

    public async Task<IReadOnlyList<Recruiter>> GetDetailsByUserIdsAsync(IEnumerable<Guid> userIds, CancellationToken cancellationToken)
    {
        return await Context.Set<Recruiter>()
            .AsNoTracking()
            .Where(r => r.UserId != null && userIds.Contains(r.UserId.Value))
            .Include(r => r.User)
            .ThenInclude(u => u!.UserSkills)!
            .ThenInclude(us => us.Skill)
            .Include(r => r.User)
            .ThenInclude(u => u!.UserInterests)!
            .ThenInclude(ui => ui.Interest)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }

    public async Task<Recruiter?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await Context.Set<Recruiter>()
            .AsNoTracking()
            .Where(r => r.UserId == userId)
            .Include(r => r.User)
            .AsSplitQuery()
            .FirstOrDefaultAsync(cancellationToken);
    }
}