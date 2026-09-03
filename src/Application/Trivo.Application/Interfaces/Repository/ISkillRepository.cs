using Trivo.Application.Pagination;
using Trivo.Domain.Common;
using Trivo.Domain.Models;

namespace Trivo.Application.Interfaces.Repository;

public interface ISkillRepository : IValidation<Skill>
{
    /// <summary>
    /// Creates a collection of skills in the database.
    /// </summary>
    /// <param name="skills">The skill entity to create.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CreateAsync(Skill skills, CancellationToken cancellationToken);

    /// <summary>
    /// Updates the list of skills associated with a specific user.
    /// </summary>
    /// <param name="userId">Unique identifier of the user whose skills will be updated.</param>
    /// <param name="skillIds">List of skill identifiers to be assigned to the user.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateAsync(Guid userId, List<Guid> skillIds, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a paged list of skills.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve (starting at 1).</param>
    /// <param name="pageSize">Number of elements per page.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation if necessary.</param>
    /// <returns>A task that returns a paged result with the requested skills.</returns>
    Task<PagedResult<Skill>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

    /// <summary>
    /// Verifies if a skill already exists with the specified name, regardless of the category.
    /// </summary>
    /// <param name="name">The name of the skill to verify.</param>
    /// <param name="cancellationToken">Token to cancel the operation early.</param>
    /// <returns>
    /// A boolean value indicating whether a skill with the given name exists (<c>true</c>) or not (<c>false</c>).
    /// </returns>
    Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken);

    /// <summary>
    /// Searches for skills whose name contains the specified text, case-insensitive.
    /// </summary>
    /// <param name="skillName">Text to search for within the skill names.</param>
    /// <param name="cancellationToken">Token to cancel the operation early.</param>
    /// <returns>A collection of skills that partially match the provided text.</returns>
    Task<IEnumerable<Skill>> SearchByNameAsync(string skillName, CancellationToken cancellationToken);
}