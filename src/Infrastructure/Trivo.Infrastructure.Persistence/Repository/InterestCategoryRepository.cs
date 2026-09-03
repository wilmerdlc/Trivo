using Microsoft.EntityFrameworkCore;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Pagination;
using Trivo.Domain.Models;
using Trivo.Infrastructure.Persistence.Base;
using Trivo.Infrastructure.Persistence.Context;

namespace Trivo.Infrastructure.Persistence.Repository;

public class InterestCategoryRepository(TrivoContext context)
    : Validation<InterestCategory>(context), IInterestCategoryRepository
{
    private readonly TrivoContext _context = context;

    public async Task CreateAsync(InterestCategory category, CancellationToken cancellationToken)
    {
        await _context.Set<InterestCategory>().AddAsync(category, cancellationToken);
    }

    public async Task<InterestCategory?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return await _context.Set<InterestCategory>()
            .AsNoTracking()
            .FirstOrDefaultAsync(ci => ci.CategoryId == categoryId, cancellationToken);
    }

    public async Task<PagedResult<InterestCategory>> GetPagedAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var total = await _context.Set<InterestCategory>().AsNoTracking().CountAsync(cancellationToken);

        var items = await _context.Set<InterestCategory>().AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<InterestCategory>(items, total, pageNumber, pageSize);
    }

    public async Task UpdateAsync(InterestCategory category, CancellationToken cancellationToken)
    {
        _context.Set<InterestCategory>().Attach(category);
        _context.Entry(category).State = EntityState.Modified;
        await Task.CompletedTask;
    }

    public async Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken)
    {
        return await Validate(x => x.Name == name, cancellationToken);
    }
}