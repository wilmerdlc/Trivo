using Trivo.Application.Utils;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Authentication;

namespace Trivo.Application.Interfaces.Services;

public interface IAuthenticationService
{
    Task<string> GenerateToken(User user, CancellationToken cancellationToken);

    string GenerateAdministratorToken(Administrator admin);

    Task<ResultT<TokenResponseDto>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);

    string GenerateRefreshToken(User user);

    string GenerateAdministratorRefreshToken(Administrator admin);
}