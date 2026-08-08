using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trivo.API.Controllers.V1.Requests;
using Trivo.Application.Features.Experts.Commands.CreateExpert;
using Trivo.Application.Features.Experts.Commands.UpdateExpert;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Expert;

namespace Trivo.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/experts")]
public class ExpertController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ResultT<ExpertDto>> CreateExpertAsync(
        [FromBody] CreateExpertCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPut("{expertId}")]
    [Authorize(Roles = "Expert")]
    public async Task<ResultT<ExpertDto>> UpdateExpertAsync(
        [FromRoute] Guid expertId,
        [FromBody] UpdateExpertRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateExpertCommand(expertId, request.AvailableForProjects, request.Hired);
        return await sender.Send(command, cancellationToken);
    }
}
