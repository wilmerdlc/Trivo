using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trivo.Application.Features.Messages.Commands.SendFile;
using Trivo.Application.Features.Messages.Commands.SendImage;
using Trivo.Application.Features.Messages.Commands.SendMessage;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Chat;

namespace Trivo.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/messages")]
public class MessageController(ISender sender) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<ResultT<MessageDto>> SendMessageAsync(
        [FromBody] SendMessageCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPost("image")]
    [Authorize]
    public async Task<ResultT<MessageDto>> SendImageAsync(
        [FromForm] SendImageCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPost("file")]
    [Authorize]
    public async Task<ResultT<MessageDto>> SendFileAsync(
        [FromForm] SendFileCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }
}
