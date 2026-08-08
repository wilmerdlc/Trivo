using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trivo.API.Controllers.V1.Requests;
using Trivo.Application.Features.Recruiters.Commands.CreateRecruiter;
using Trivo.Application.Features.Recruiters.Commands.UpdateRecruiter;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Recruiter;

namespace Trivo.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/recruiters")]
public class RecruiterController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ResultT<RecruiterDto>> CreateRecruiterAsync(
        [FromBody] CreateRecruiterCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPut("{recruiterId}")]
    [Authorize(Roles = "Recruiter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ResultT<RecruiterDto>> UpdateRecruiterAsync(
        [FromRoute] Guid recruiterId,
        [FromBody] UpdateRecruiterRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateRecruiterCommand(recruiterId, request.CompanyName);
        return await sender.Send(command, cancellationToken);
    }
}
