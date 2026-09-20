using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Commands.UpdateUsername;

public sealed record UpdateUsernameCommand(
    Guid UserId,
    string Username
) : ICommand<UpdateUsernameDto>, IUserOwnedRequest;
