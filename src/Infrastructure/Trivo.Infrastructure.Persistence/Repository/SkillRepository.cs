using Microsoft.EntityFrameworkCore;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Pagination;
using Trivo.Domain.Models;
using Trivo.Infrastructure.Persistence.Base;
using Trivo.Infrastructure.Persistence.Context;

namespace Trivo.Infrastructure.Persistence.Repository;

public class SkillRepository(TrivoContext context) : Validation<Skill>(context), ISkillRepository
{
    private readonly TrivoContext _context = context;

    public async Task CreateAsync(Skill skills, CancellationToken cancellationToken)
    {
        await _context.Set<Skill>().AddAsync(skills, cancellationToken);
    }

    public async Task UpdateAsync(Guid userId, List<Guid> skillIds, CancellationToken cancellationToken)
    {
        var currentSkillIds = await _context.Set<UserSkill>()
            .Where(us => us.UserId == userId && us.SkillId != null)
            .Select(us => us.SkillId!.Value)
            .ToListAsync(cancellationToken);

        var currentSet = currentSkillIds.ToHashSet();
        var newSet = skillIds.ToHashSet();

        var skillsToRemove = currentSet.Except(newSet).ToList();
        var skillsToAdd = newSet.Except(currentSet).ToList();

        // Remove old relationships
        var relationshipsToRemove = await _context.Set<UserSkill>()
            .Where(us => us.UserId == userId && skillsToRemove.Contains(us.SkillId!.Value))
            .ToListAsync(cancellationToken);

        _context.Set<UserSkill>().RemoveRange(relationshipsToRemove);

        // Add new relationships
        var newRelationships = skillsToAdd.Select(id => new UserSkill
        {
            UserId = userId,
            SkillId = id
        });

        await _context.Set<UserSkill>().AddRangeAsync(newRelationships, cancellationToken);
    }


    public async Task<PagedResult<Skill>> GetPagedAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var total = await _context.Set<Skill>().AsNoTracking().CountAsync(cancellationToken);

        var items = await _context.Set<Skill>().AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Skill>(items, total, pageNumber, pageSize);
    }

    public async Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken)
    {
        return await Validate(x => x.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<Skill>> SearchByNameAsync(string skillName, CancellationToken cancellationToken)
    {
        return await _context.Set<Skill>()
            .AsNoTracking()
            .Where(s => EF.Functions.ILike(s.Name!, $"%{skillName}%"))
            .ToListAsync(cancellationToken);
    }
}