using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Trivo.Application.Interfaces.Repository.Base;
using Trivo.Application.Pagination;
using Trivo.Infrastructure.Persistence.Context;

namespace Trivo.Infrastructure.Persistence.Base;

public class GenericRepository<TEntity>(TrivoContext context) : IGenericRepository<TEntity>
    where TEntity : class
{
    protected readonly TrivoContext Context = context;

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await Context.Set<TEntity>().FindAsync([id], cancellationToken);

    public async Task<PagedResult<TEntity>> GetPagedAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var total = await Context.Set<TEntity>().AsNoTracking().CountAsync(cancellationToken);

        var items = await Context.Set<TEntity>().AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<TEntity>(items, total, pageNumber, pageSize);
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Context.Set<TEntity>().Attach(entity);
        Context.Entry(entity).State = EntityState.Modified;
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Context.Set<TEntity>().Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<bool> ValidateAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken) =>
        await Context.Set<TEntity>()
            .AsNoTracking()
            .AnyAsync(predicate, cancellationToken);
}