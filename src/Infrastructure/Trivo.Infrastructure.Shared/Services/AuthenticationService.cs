using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Utils;
using Trivo.Domain.Configurations;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Authentication;

namespace Trivo.Infrastructure.Shared.Services;

public class AuthenticationService(
    IOptions<JwtSetting> configurations,
    IUserRoleService userRoleService,
    IUserRepository userRepository,
    IAdministratorRepository administratorRepository,
    IGetExpertIdService getExpertIdService,
    IGetRecruiterIdService getRecruiterIdService
) : IAuthenticationService
{
    private readonly JwtSetting _configurations = configurations.Value;

    public async Task<string> GenerateToken(User user, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim("username", user.Username!),
            new Claim("type", "access")
        };

        // Add Roles
        var roles = await userRoleService.GetRolesAsync(user.Id, cancellationToken);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.ToString())));

        // Get expert id if applicable
        var expertId = await getExpertIdService.GetExpertIdAsync(user.Id, cancellationToken);
        if (expertId.HasValue)
        {
            claims.Add(new Claim("expertId", expertId.Value.ToString()));
        }

        // Get recruiter id if applicable
        var recruiterId = await getRecruiterIdService.GetRecruiterIdAsync(user.Id, cancellationToken);
        if (recruiterId.HasValue)
        {
            claims.Add(new Claim("recruiterId", recruiterId.Value.ToString()));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurations.Key!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configurations.Issuer,
            audience: _configurations.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_configurations.DurationInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateAdministratorToken(Administrator admin)
    {
        var fullName = $"{admin.FirstName} {admin.LastName}";
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, admin.Id.ToString()!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, admin.Email!),
            new Claim("username", admin.Username!),
            new Claim("fullName", fullName),
            new Claim("profilePicture", admin.ProfilePicture!),
            new Claim("roles", Roles.Administrator.ToString()),
            new Claim("type", "access")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurations.Key!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configurations.Issuer,
            audience: _configurations.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_configurations.DurationInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<ResultT<TokenResponseDto>> RefreshTokenAsync(string refreshToken,
        CancellationToken cancellationToken)
    {
        var handler = new JwtSecurityTokenHandler();

        var validatedTokenPrincipal = handler.ValidateToken(refreshToken, new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = _configurations.Issuer,
            ValidAudience = _configurations.Audience,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurations.Key!)),
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwt = (JwtSecurityToken)validatedToken;

        if (jwt.Claims.FirstOrDefault(c => c.Type == "type")?.Value != "refresh")
            return ResultT<TokenResponseDto>.Failure(Error.Unauthorized("Token.Invalid",
                "The token is not a refresh type."));

        var userId = jwt.Subject;
        var entity = jwt.Claims.FirstOrDefault(c => c.Type == "entity")?.Value;

        if (entity == Roles.Administrator.ToString())
        {
            var admin = await administratorRepository.GetByIdAsync(Guid.Parse(userId), cancellationToken);
            if (admin is null)
                return ResultT<TokenResponseDto>.Failure(Error.NotFound("404", "Administrator not found."));

            var newAccessToken = GenerateAdministratorToken(admin);
            var newRefreshToken = GenerateAdministratorRefreshToken(admin);

            return ResultT<TokenResponseDto>.Success(new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            });
        }

        if (entity == Roles.User.ToString())
        {
            var user = await userRepository.GetByIdAsync(Guid.Parse(userId), cancellationToken);
            if (user is null)
                return ResultT<TokenResponseDto>.Failure(Error.NotFound("404", "User not found."));

            var newAccessToken = await GenerateToken(user, cancellationToken);
            var newRefreshToken = GenerateRefreshToken(user);

            return ResultT<TokenResponseDto>.Success(new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            });
        }

        return ResultT<TokenResponseDto>.Failure(Error.Unauthorized("Token.Invalid",
            "The token does not contain valid entity information."));
    }

    public string GenerateRefreshToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("entity", Roles.User.ToString()),
            new Claim("type", "refresh")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurations.Key!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configurations.Issuer,
            audience: _configurations.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateAdministratorRefreshToken(Administrator admin)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, admin.Id.ToString()!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("entity", Roles.Administrator.ToString()),
            new Claim("type", "refresh")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurations.Key!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configurations.Issuer,
            audience: _configurations.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}