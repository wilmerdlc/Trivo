using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    Guid UserId,
    string Username,
    string Email
) : ICommand<UpdateUserDto>, IUserOwnedRequest;
