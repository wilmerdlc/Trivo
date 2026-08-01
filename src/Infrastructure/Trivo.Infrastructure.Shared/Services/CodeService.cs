using Microsoft.Extensions.Logging;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Email;

namespace Trivo.Infrastructure.Shared.Services;

public class CodeService(
    ILogger<CodeService> logger,
    IUserRepository userRepository,
    ICodeRepository codeRepository,
    IUnitOfWork unitOfWork
) : ICodeService
{
    public async Task<ResultT<string>> GenerateCodeAsync(Guid userId, CodeType codeType, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("No user was found with the provided ID: {UserId}", userId);

            return ResultT<string>.Failure(Error.NotFound("404", "User not found"));
        }

        if (codeType == CodeType.AccountConfirmation && user.IsAccountConfirmed == true)
        {
            logger.LogWarning("User with ID {UserId} already has a confirmed account.", userId);

            return ResultT<string>.Failure(Error.Conflict("409", "The account has already been confirmed."));
        }

        var generatedCode = CodeGenerator.GenerateNumericCode();

        Code code = new()
        {
            CodeId = Guid.NewGuid(),
            UserId = user.Id,
            Value = generatedCode,
            ExpiresAt = DateTime.UtcNow.AddMinutes(10),
            Type = codeType.ToString()
        };

        await codeRepository.CreateAsync(code, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "Successfully generated and saved a new verification code for user with ID {UserId}",
            user.Id
        );

        return ResultT<string>.Success(code.Value!);
    }

    public async Task<ResultT<CodeDto>> GetCodeAsync(Guid codeId, CancellationToken cancellationToken)
    {
        var code = await codeRepository.GetByIdAsync(codeId, cancellationToken);
        if (code is null)
        {
            logger.LogWarning("No code was found with the provided ID: {CodeId}", codeId);

            return ResultT<CodeDto>.Failure(Error.NotFound("404", "Code not found"));
        }

        CodeDto codeDto = new(
            CodeId: code.CodeId ?? Guid.Empty,
            UserId: code.UserId ?? Guid.Empty,
            Code: code.Value!,
            IsUsed: code.IsUsed!.Value,
            Expiration: code.ExpiresAt
        );

        logger.LogInformation(
            "Successfully retrieved code with ID {CodeId} for user {UserId}",
            codeId,
            code.UserId
        );

        return ResultT<CodeDto>.Success(codeDto);
    }

    public async Task<Result> DeleteCodeAsync(Guid codeId, CancellationToken cancellationToken)
    {
        var code = await codeRepository.GetByIdAsync(codeId, cancellationToken);
        if (code is null)
        {
            logger.LogWarning("No code was found with the provided ID: {CodeId}", codeId);

            return Result.Failure(Error.NotFound("404", "Code not found"));
        }

        await codeRepository.DeleteAsync(code, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "Successfully deleted code with ID {CodeId} for user {UserId}",
            code.CodeId,
            code.UserId
        );

        return Result.Success();
    }

    public async Task<Result> ConfirmAccountAsync(Guid userId, string code, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("No user was found with the provided ID: {UserId}", userId);

            return Result.Failure(Error.NotFound("404", "User not found"));
        }

        var codeEntity = await codeRepository.FindAsync(code, cancellationToken);
        if (codeEntity is null)
        {
            logger.LogWarning("No code was found with the value: {Code}", code);

            return Result.Failure(Error.NotFound("404", "Code not found"));
        }

        if (codeEntity.UserId != user.Id)
        {
            logger.LogWarning("Code with value {Code} does not belong to user with ID {UserId}", code, userId);

            return Result.Failure(Error.Unauthorized("403", "The code does not belong to this user"));
        }

        if (codeEntity.IsUsed is true)
        {
            logger.LogWarning("Code with value {Code} has already been used", code);

            return Result.Failure(Error.Conflict("409", "This code has already been used"));
        }

        var isValid = await codeRepository.IsValidAsync(code, cancellationToken);
        if (!isValid)
        {
            logger.LogWarning("Code with value {Code} has expired or is not valid", code);

            return Result.Failure(Error.Failure("400", "The code has expired or is not valid"));
        }

        await codeRepository.MarkAsUsedAsync(codeEntity.Value!, cancellationToken);

        user.IsAccountConfirmed = true;
        await userRepository.UpdateAsync(user, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("User with ID {UserId} confirmed their account successfully", userId);

        return Result.Success();
    }

    public async Task<Result> IsCodeAvailableAsync(string code, CancellationToken cancellationToken)
    {
        var isUnused = await codeRepository.IsUnusedAsync(code, cancellationToken);

        if (!isUnused)
        {
            logger.LogWarning("Code '{Code}' has already been used.", code);

            return Result.Failure(Error.Conflict("409", "The code has already been used"));
        }

        logger.LogInformation("Code '{Code}' is available for use.", code);

        return Result.Success();
    }

    public async Task<ResultT<string>> ValidateCodeAsync(string code, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(code))
        {
            logger.LogWarning("Attempted to validate an empty or null code.");

            return ResultT<string>.Failure(Error.Failure("400", "A valid code must be provided."));
        }

        var isValid = await codeRepository.IsValidAsync(code, cancellationToken);

        if (!isValid)
        {
            logger.LogWarning("Code '{Code}' has expired or is not valid.", code);

            return ResultT<string>.Failure(Error.Failure("400", "The code has expired or is not valid."));
        }

        logger.LogInformation("Code '{Code}' was validated successfully.", code);

        return ResultT<string>.Success("The code is valid.");
    }
}
