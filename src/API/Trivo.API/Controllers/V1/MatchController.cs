using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trivo.Application.Features.Matching.Commands.CreateMatch;
using Trivo.Application.Features.Matching.Commands.CreateMatchRejection;
using Trivo.Application.Features.Matching.Commands.UpdateMatch;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Matching;

namespace Trivo.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/matches")]
public class MatchController(ISender sender) : ControllerBase
{
    [HttpPost]
    // [Authorize]
    public async Task<ResultT<MatchDetailsDto>> CreateMatchAsync(
        [FromBody] CreateMatchingCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPut]
    // [Authorize]
    public async Task<ResultT<MatchDetailsDto>> UpdateMatchStatusAsync(
        [FromBody] UpdateMatchingCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPost("reject")]
    // [Authorize]
    public async Task<ResultT<string>> RejectMatchAsync(
        [FromBody] CreateMatchRejectionCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }
}
