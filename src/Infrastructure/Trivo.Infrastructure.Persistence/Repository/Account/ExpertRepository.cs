using Microsoft.EntityFrameworkCore;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Domain.Models;
using Trivo.Infrastructure.Persistence.Base;
using Trivo.Infrastructure.Persistence.Context;

namespace Trivo.Infrastructure.Persistence.Repository.Account;

public class ExpertRepository(TrivoContext context) : GenericRepository<Expert>(context), IExpertRepository
{
    public async Task<IEnumerable<Expert>> GetBySkillsAndInterestsAsync(
        List<Guid> skillIds,
        List<Guid> interestIds,
        CancellationToken cancellationToken
    )
    {
        return await Context.Set<Expert>()
            .AsNoTracking()
            .Include(e => e.User)
            .ThenInclude(u => u!.UserSkills)
            .Include(e => e.User)
            .ThenInclude(u => u!.UserInterests)
            .AsSplitQuery()
            .Where(e =>
                e.User != null &&
                (skillIds.Count == 0 ||
                 e.User.UserSkills!
                     .Any(us => skillIds.Contains((Guid)us.SkillId!))) &&
                (interestIds.Count == 0 ||
                 e.User.UserInterests!
                     .Any(ui => interestIds.Contains((Guid)ui.InterestId!))))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsUserExpertAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await ValidateAsync(x => x.UserId == userId, cancellationToken);
    }

    public async Task<Expert?> GetDetailsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await Context.Set<Expert>()
            .AsNoTracking()
            .Include(e => e.User)
            .ThenInclude(u => u!.UserSkills)!
            .ThenInclude(us => us.Skill)
            .Include(e => e.User)
            .ThenInclude(u => u!.UserInterests)!
            .ThenInclude(ui => ui.Interest)
            .FirstOrDefaultAsync(e => e.UserId == userId, cancellationToken);
    }

    public async Task<IReadOnlyList<Expert>> GetDetailsByUserIdsAsync(IEnumerable<Guid> userIds, CancellationToken cancellationToken)
    {
        return await Context.Set<Expert>()
            .AsNoTracking()
            .Where(e => e.UserId != null && userIds.Contains(e.UserId.Value))
            .Include(e => e.User)
            .ThenInclude(u => u!.UserSkills)!
            .ThenInclude(us => us.Skill)
            .Include(e => e.User)
            .ThenInclude(u => u!.UserInterests)!
            .ThenInclude(ui => ui.Interest)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }

    public async Task<Expert?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await Context.Set<Expert>()
            .AsNoTracking()
            .Where(e => e.UserId == userId)
            .Include(e => e.User)
            .ThenInclude(u => u!.UserSkills)!
            .ThenInclude(us => us.Skill)
            .Include(e => e.User)
            .ThenInclude(u => u!.UserInterests)!
            .ThenInclude(ui => ui.Interest)
            .AsSplitQuery()
            .FirstOrDefaultAsync(cancellationToken);
    }
}