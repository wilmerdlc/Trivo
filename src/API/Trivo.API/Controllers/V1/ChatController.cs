using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trivo.Application.Features.Chat.Commands.CreateChat;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Chat;

namespace Trivo.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/chats")]
public class ChatController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ResultT<ChatDto>> CreateChatAsync(
        [FromBody] CreateChatCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }
}
