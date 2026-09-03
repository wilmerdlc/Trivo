using Microsoft.AspNetCore.Http;
using Trivo.Application.Abstractions.Messages;

namespace Trivo.Application.Features.Users.Commands.UpdateProfilePicture;

public sealed record UpdateProfilePictureCommand(
    Guid UserId,
    IFormFile Image
) : ICommand<string>, IUserOwnedRequest;
