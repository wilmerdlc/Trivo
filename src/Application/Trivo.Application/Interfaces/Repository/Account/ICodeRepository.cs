using Trivo.Domain.Models;

namespace Trivo.Application.Interfaces.Repository.Account;

public interface ICodeRepository
{
    /// <summary>
    /// Creates a new code.
    /// </summary>
    /// <param name="code">The code object to create.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task CreateAsync(Code code, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a code by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the code.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The found code, or null if it does not exist.</returns>
    Task<Code?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Searches for a code by its value.
    /// </summary>
    /// <param name="code">The code value to search for.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The found code, or null if it does not exist.</returns>
    Task<Code?> FindAsync(string code, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a code.
    /// </summary>
    /// <param name="code">The code object to delete.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task DeleteAsync(Code code, CancellationToken cancellationToken);

    /// <summary>
    /// Verifies if a specific code exists.
    /// </summary>
    /// <param name="code">The code value to verify.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>True if it exists, false otherwise.</returns>
    Task<bool> ExistsAsync(string code, CancellationToken cancellationToken);

    /// <summary>
    /// Verifies if a code is valid.
    /// </summary>
    /// <param name="code">The code value to validate.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>True if the code is valid, false otherwise.</returns>
    Task<bool> IsValidAsync(string code, CancellationToken cancellationToken);

    /// <summary>
    /// Marks a code as used.
    /// </summary>
    /// <param name="code">The code value to mark.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task MarkAsUsedAsync(string code, CancellationToken cancellationToken);

    /// <summary>
    /// Verifies if a code has not been used yet.
    /// </summary>
    /// <param name="code">The code value to verify.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>True if the code has not been used, false otherwise.</returns>
    Task<bool> IsUnusedAsync(string code, CancellationToken cancellationToken);
}