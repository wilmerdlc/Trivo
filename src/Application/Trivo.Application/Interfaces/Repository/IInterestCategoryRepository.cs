using Trivo.Application.Pagination;
using Trivo.Domain.Common;
using Trivo.Domain.Models;

namespace Trivo.Application.Interfaces.Repository;

/// <summary>
/// Defines operations to manage interest categories within the system.
/// </summary>
public interface IInterestCategoryRepository : IValidation<InterestCategory>
{
    /// <summary>
    /// Creates an interest category in the database.
    /// </summary>
    /// <param name="category">The interest category to create.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation if necessary.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CreateAsync(InterestCategory category, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a specific interest category by its unique identifier.
    /// </summary>
    /// <param name="interestCategoryId">The identifier of the interest category to search for.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A task that returns the found interest category.</returns>
    Task<InterestCategory?> GetByIdAsync(Guid interestCategoryId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a paged list of existing interest categories in the database.
    /// </summary>
    /// <param name="pageNumber">The page number to query (starting at 1).</param>
    /// <param name="pageSize">The number of elements per page.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A task that returns a paged result containing interest categories.</returns>
    Task<PagedResult<InterestCategory>> GetPagedAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing interest category in the database.
    /// </summary>
    /// <param name="interestCategory">The interest category entity with the updated values.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateAsync(InterestCategory interestCategory, CancellationToken cancellationToken);

    /// <summary>
    /// Verifies if a record with the specified name already exists.
    /// </summary>
    /// <param name="name">The name to verify in the database.</param>
    /// <param name="cancellationToken">Token to cancel the operation if necessary.</param>
    /// <returns>
    /// <c>true</c> if a record with that name exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken);
}