using Trivo.Application.Interfaces.Repository.Base;
using Trivo.Domain.Models;

namespace Trivo.Application.Interfaces.Repository.Account;

/// <summary>
/// Defines specific operations for the <see cref="Expert"/> entity, 
/// including filtering by skills and interests.
/// </summary>
public interface IExpertRepository : IGenericRepository<Expert>
{
    /// <summary>
    /// Filters experts who possess at least one of the specified skills or interests.
    /// </summary>
    /// <param name="skillIds">List of skill IDs to filter by.</param>
    /// <param name="interestIds">List of interest IDs to filter by.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>
    /// A collection of experts matching the provided skill and interest criteria.
    /// </returns>
    Task<IEnumerable<Expert>> GetBySkillsAndInterestsAsync(
        List<Guid> skillIds,
        List<Guid> interestIds,
        CancellationToken cancellationToken);

    /// <summary>
    /// Verifies if the user is registered as an expert.
    /// </summary>
    Task<bool> IsUserExpertAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the details of an expert by user ID.
    /// </summary>
    Task<Expert?> GetDetailsAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the details of every expert whose user ID is in the given list, in one query.
    /// </summary>
    Task<IReadOnlyList<Expert>> GetDetailsByUserIdsAsync(IEnumerable<Guid> userIds, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the expert record associated with a specific user ID.
    /// </summary>
    Task<Expert?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}