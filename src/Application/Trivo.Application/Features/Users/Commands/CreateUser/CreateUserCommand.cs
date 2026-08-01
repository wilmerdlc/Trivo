using Microsoft.AspNetCore.Http;
using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string? FirstName,
    string? LastName,
    string? Biography,
    string? Email,
    string? Password,
    string? Username,
    string? Location,
    string? Position,
    List<Guid>? InterestIds,
    IFormFile? Photo
) : ICommand<UserDto>;
