using Trivo.Application.Utils;
using Trivo.Domain.Enums;

using Trivo.Application.DTOs.Email;

namespace Trivo.Application.Interfaces.Services;

/// <summary>
/// Service responsible for the generation, validation, and management of codes for users.
/// </summary>
public interface ICodeService
{
    /// <summary>
    /// Generates a new code for a user with a specific type (e.g., account confirmation, password recovery).
    /// </summary>
    /// <param name="userId">ID of the user for whom the code will be generated.</param>
    /// <param name="codeType">Type of code to generate.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A result containing the generated code as a string.</returns>
    Task<ResultT<string>> GenerateCodeAsync(Guid userId, CodeType codeType, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the details of a code based on its ID.
    /// </summary>
    /// <param name="codeId">ID of the code to search for.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A result containing the DTO with the code data.</returns>
    Task<ResultT<CodeDto>> GetCodeAsync(Guid codeId, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an existing code based on its ID.
    /// </summary>
    /// <param name="codeId">ID of the code to delete.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The result of the operation.</returns>
    Task<Result> DeleteCodeAsync(Guid codeId, CancellationToken cancellationToken);

    /// <summary>
    /// Confirms a user's account using a validation code.
    /// </summary>
    /// <param name="userId">ID of the user who wishes to confirm their account.</param>
    /// <param name="code">The code received by the user.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The result of the operation.</returns>
    Task<Result> ConfirmAccountAsync(Guid userId, string code, CancellationToken cancellationToken);

    /// <summary>
    /// Verifies if a code is available (valid and not expired).
    /// </summary>
    /// <param name="code">The code to verify.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The result of the operation.</returns>
    Task<Result> IsCodeAvailableAsync(string code, CancellationToken cancellationToken);

    /// <summary>
    /// Validates a password-recovery code for the given user (belongs to them, correct type,
    /// unused, not expired) and marks it as used. Callers must only reset the password after
    /// this succeeds.
    /// </summary>
    /// <param name="userId">ID of the user requesting the password reset.</param>
    /// <param name="code">The code received by the user.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The result of the operation.</returns>
    Task<Result> ValidatePasswordRecoveryCodeAsync(Guid userId, string code, CancellationToken cancellationToken);

    /// <summary>
    /// Validates if a code is correct and active.
    /// </summary>
    /// <param name="code">The code to validate.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A result indicating whether the code is valid.</returns>
    Task<ResultT<string>> ValidateCodeAsync(string code, CancellationToken cancellationToken);
}