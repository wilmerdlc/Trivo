using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;

using Trivo.Application.DTOs.Authentication;

namespace Trivo.Application.Features.Users.Commands.LoginUser;

internal sealed class LoginUserCommandHandler(
    IAuthenticationService authenticationService,
    IUserRepository userRepository,
    ILogger<LoginUserCommandHandler> logger
) : ICommandHandler<LoginUserCommand, TokenResponseDto>
{
    public async Task<ResultT<TokenResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            logger.LogWarning("Login failed: no user found with email '{Email}'.", request.Email);

            return ResultT<TokenResponseDto>.Failure(
                Error.NotFound("404", "User not found.")
            );
        }

        if (user.UserStatus == nameof(UserStatus.Banned))
        {
            logger.LogWarning("User with email '{Email}' is banned and cannot log in.", user.Email);

            return ResultT<TokenResponseDto>.Failure(
                Error.Conflict("409", "The user has been banned and cannot log in.")
            );
        }

        if (!await userRepository.IsAccountConfirmedAsync(user.Id, cancellationToken))
        {
            logger.LogWarning("Login failed: account not confirmed for user with ID '{UserId}'.", user.Id);

            return ResultT<TokenResponseDto>.Failure(
                Error.Conflict("409", "The account has not been confirmed.")
            );
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            logger.LogWarning("Login failed: invalid password for user with ID '{UserId}'.", user.Id);

            return ResultT<TokenResponseDto>.Failure(
                Error.Conflict("409", "Invalid password.")
            );
        }

        var accessToken = await authenticationService.GenerateToken(user, cancellationToken);
        var refreshToken = authenticationService.GenerateRefreshToken(user);

        logger.LogInformation(
            "Login successful for user with ID '{Id}' and email '{Email}'.",
            user.Id,
            user.Email
        );

        return ResultT<TokenResponseDto>.Success(new TokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }
}
