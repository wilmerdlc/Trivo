using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Authentication;

namespace Trivo.Application.Features.Users.Commands.LoginUser;

internal sealed class LoginUserCommandHandler(
    IAuthenticationService authenticationService,
    IAccountAccessService accountAccessService,
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

        // Checked only after the password is verified: the sanction reason is private to the
        // account owner, so it must not be readable by anyone who merely knows the email.
        var accessError = await accountAccessService.GetAccessErrorAsync(user, cancellationToken);
        if (accessError is not null)
        {
            logger.LogWarning("Login blocked for user '{UserId}': {Code}.", user.Id, accessError.Code);

            return ResultT<TokenResponseDto>.Failure(accessError);
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
