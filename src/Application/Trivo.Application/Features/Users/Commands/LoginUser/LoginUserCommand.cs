using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Authentication;

namespace Trivo.Application.Features.Users.Commands.LoginUser;

public sealed record LoginUserCommand(
    string Email,
    string Password
) : ICommand<TokenResponseDto>;
