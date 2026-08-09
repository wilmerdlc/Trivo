using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Authentication;

namespace Trivo.Application.Features.Administrator.Commands.LoginAdmin;

public sealed record AdminLoginCommand(
    string Email,
    string Password
) : ICommand<TokenResponseDto>;