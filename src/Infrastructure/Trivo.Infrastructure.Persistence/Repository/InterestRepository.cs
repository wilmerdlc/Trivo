using Microsoft.EntityFrameworkCore;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Pagination;
using Trivo.Domain.Models;
using Trivo.Infrastructure.Persistence.Base;
using Trivo.Infrastructure.Persistence.Context;

namespace Trivo.Infrastructure.Persistence.Repository;

public class InterestRepository(TrivoContext context)
    : Validation<Interest>(context), IInterestRepository
{
    private readonly TrivoContext _context = context;

    public async Task AddAsync(Interest interest, CancellationToken cancellationToken)
    {
        await _context.Set<Interest>().AddAsync(interest, cancellationToken);
    }

    public async Task UpdateUserInterestsAsync(
        Guid userId,
        List<Guid> interestIds,
        CancellationToken cancellationToken)
    {
        var currentInterestIds = await _context.Set<UserInterest>()
            .Where(ui => ui.UserId == userId && ui.InterestId.HasValue)
            .Select(ui => ui.InterestId!.Value)
            .ToListAsync(cancellationToken);

        var incomingSet = interestIds.ToHashSet();
        var currentSet = currentInterestIds.ToHashSet();

        var interestsToRemove = currentSet.Except(incomingSet).ToList();
        var newInterestIds = incomingSet.Except(currentSet).ToList();

        var relationshipsToRemove = await _context.Set<UserInterest>()
            .Where(ui =>
                ui.UserId == userId &&
                ui.InterestId.HasValue &&
                interestsToRemove.Contains(ui.InterestId.Value))
            .ToListAsync(cancellationToken);

        _context.Set<UserInterest>().RemoveRange(relationshipsToRemove);

        var newRelationships = newInterestIds.Select(id => new UserInterest
        {
            UserId = userId,
            InterestId = id
        });

        await _context.Set<UserInterest>()
            .AddRangeAsync(newRelationships, cancellationToken);
    }

    public async Task<PagedResult<Interest>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var total = await _context.Set<Interest>()
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var items = await _context.Set<Interest>()
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Interest>(items, total, pageNumber, pageSize);
    }

    public async Task<PagedResult<Interest>> GetPagedByCategoriesAsync(
        IEnumerable<Guid> categoryIds,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var idSet = categoryIds.ToHashSet();

        var query = _context.Set<Interest>()
            .AsNoTracking()
            .Where(i => i.CategoryId.HasValue && idSet.Contains(i.CategoryId.Value));

        var total = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Interest>(items, total, pageNumber, pageSize);
    }

    public async Task<PagedResult<Interest>> GetSimplePagedByCategoriesAsync(
        IEnumerable<Guid> categoryIds,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var idSet = categoryIds.ToHashSet();

        var query = _context.Set<Interest>()
            .AsNoTracking()
            .Where(i => i.CategoryId.HasValue && idSet.Contains(i.CategoryId.Value));

        var total = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(i => new Interest
            {
                Id = i.Id,
                Name = i.Name
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<Interest>(items, total, pageNumber, pageSize);
    }

    public async Task<Interest?> GetByIdAsync(Guid interestId, CancellationToken cancellationToken)
    {
        return await _context.Set<Interest>()
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == interestId, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await Validate(x => x.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsByNameAndCategoryAsync(
        string name,
        Guid categoryId,
        CancellationToken cancellationToken)
    {
        return await Validate(
            x => x.Name == name && x.CategoryId == categoryId,
            cancellationToken);
    }

    public async Task<IEnumerable<Interest>> SearchByNameAsync(
        string name,
        CancellationToken cancellationToken)
    {
        return await _context.Set<Interest>()
            .AsNoTracking()
            .Where(i => EF.Functions.ILike(i.Name!, $"%{name}%"))
            .ToListAsync(cancellationToken);
    }
}