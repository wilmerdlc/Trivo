using Microsoft.AspNetCore.Http;
using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Chat;

namespace Trivo.Application.Features.Messages.Commands.SendImage;

public sealed record SendImageCommand(
    Guid ChatId,
    Guid SenderId,
    Guid ReceiverId,
    IFormFile Image
) : ICommand<MessageDto>;
