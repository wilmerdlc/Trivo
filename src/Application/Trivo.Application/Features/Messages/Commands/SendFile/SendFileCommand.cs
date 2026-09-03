using Microsoft.AspNetCore.Http;
using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Chat;

namespace Trivo.Application.Features.Messages.Commands.SendFile;

public sealed record SendFileCommand(
    Guid ChatId,
    Guid SenderId,
    Guid ReceiverId,
    IFormFile File
) : ICommand<MessageDto>, IUserOwnedRequest
{
    Guid IUserOwnedRequest.UserId => SenderId;
}
