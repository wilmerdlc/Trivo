using System.Linq.Expressions;
using Trivo.Application.Pagination;

namespace Trivo.Application.Interfaces.Repository.Base;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<PagedResult<TEntity>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

    Task CreateAsync(TEntity entity, CancellationToken cancellationToken);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);

    Task<bool> ValidateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
}