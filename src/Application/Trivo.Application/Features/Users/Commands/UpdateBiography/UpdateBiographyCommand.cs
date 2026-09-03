using Trivo.Application.Abstractions.Messages;

namespace Trivo.Application.Features.Users.Commands.UpdateBiography;

public sealed record UpdateBiographyCommand(
    Guid UserId,
    string Biography
) : ICommand<string>, IUserOwnedRequest;
