using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Authentication;

namespace Trivo.Application.Features.Administrator.Commands.LoginAdmin;

internal sealed class AdminLoginCommandHandler(
    IAuthenticationService authenticationService,
    IAdministratorRepository adminRepository,
    ILogger<AdminLoginCommandHandler> logger
) : ICommandHandler<AdminLoginCommand, TokenResponseDto>
{
    public async Task<ResultT<TokenResponseDto>> Handle(AdminLoginCommand request, CancellationToken cancellationToken)
    {
        var admin = await adminRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (admin is null)
        {
            logger.LogWarning("Login failed: no administrator found with email '{Email}'.", request.Email);

            return ResultT<TokenResponseDto>.Failure(
                Error.NotFound("404", "Administrator not found.")
            );
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, admin.PasswordHash))
        {
            logger.LogWarning("Login failed: invalid password for administrator with email '{Email}'.", request.Email);

            return ResultT<TokenResponseDto>.Failure(
                Error.Conflict("409", "Invalid password.")
            );
        }

        var accessToken = authenticationService.GenerateAdministratorToken(admin);
        var refreshToken = authenticationService.GenerateAdministratorRefreshToken(admin);
        
        logger.LogInformation(
            "Login successful for administrator with ID '{Id}' and email '{Email}'.",
            admin.Id,
            admin.Email
        );

        return ResultT<TokenResponseDto>.Success(new TokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }
}