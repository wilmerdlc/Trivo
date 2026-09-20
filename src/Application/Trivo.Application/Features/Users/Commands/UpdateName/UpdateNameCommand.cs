using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Commands.UpdateName;

public sealed record UpdateNameCommand(
    Guid UserId,
    string FirstName,
    string LastName
) : ICommand<UpdateNameDto>, IUserOwnedRequest;
