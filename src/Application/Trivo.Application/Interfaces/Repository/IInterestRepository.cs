using Trivo.Application.Pagination;
using Trivo.Domain.Common;
using Trivo.Domain.Models;

namespace Trivo.Application.Interfaces.Repository;

/// <summary>
/// Provides persistence operations for managing <see cref="Interest"/> entities
/// and their relationships with users.
/// </summary>
public interface IInterestRepository : IValidation<Interest>
{
    /// <summary>
    /// Adds a new interest entity to the data store.
    /// </summary>
    /// <param name="interest">The interest entity to add.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task AddAsync(Interest interest, CancellationToken cancellationToken);

    /// <summary>
    /// Replaces all interests assigned to a specific user with a new list.
    /// Removes old relations and inserts the new ones.
    /// </summary>
    /// <param name="userId">Identifier of the user.</param>
    /// <param name="interestIds">List of interest identifiers to assign.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task UpdateUserInterestsAsync(Guid userId, List<Guid> interestIds, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a paginated list of all interests.
    /// </summary>
    /// <param name="pageNumber">Page index starting from 1.</param>
    /// <param name="pageSize">Number of records per page.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Paged result containing interests.</returns>
    Task<PagedResult<Interest>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a paginated list of interests filtered by one or more category identifiers.
    /// </summary>
    /// <param name="categoryIds">Collection of category identifiers.</param>
    /// <param name="pageNumber">Page index starting from 1.</param>
    /// <param name="pageSize">Number of records per page.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task<PagedResult<Interest>> GetPagedByCategoriesAsync(IEnumerable<Guid> categoryIds, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves an interest by its unique identifier.
    /// </summary>
    /// <param name="interestId">Interest identifier.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The interest if found; otherwise null.</returns>
    Task<Interest?> GetByIdAsync(Guid interestId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a paginated list of interests filtered by categories,
    /// returning only basic fields (lightweight projection).
    /// </summary>
    /// <param name="categoryIds">Category identifiers.</param>
    /// <param name="pageNumber">Page index starting from 1.</param>
    /// <param name="pageSize">Number of records per page.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task<PagedResult<Interest>> GetSimplePagedByCategoriesAsync(IEnumerable<Guid> categoryIds, int pageNumber,
        int pageSize, CancellationToken cancellationToken);

    /// <summary>
    /// Checks whether an interest exists with the specified name.
    /// </summary>
    /// <param name="name">Name to validate.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>True if it exists; otherwise false.</returns>
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);

    /// <summary>
    /// Checks whether an interest exists with the specified name within a specific category.
    /// </summary>
    /// <param name="name">Interest name.</param>
    /// <param name="categoryId">Category identifier.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>True if it exists; otherwise false.</returns>
    Task<bool> ExistsByNameAndCategoryAsync(string name, Guid categoryId, CancellationToken cancellationToken);

    /// <summary>
    /// Searches interests whose names contain the provided text (case-insensitive).
    /// </summary>
    /// <param name="name">Search text.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Collection of matching interests.</returns>
    Task<IEnumerable<Interest>> SearchByNameAsync(string name, CancellationToken cancellationToken);
}