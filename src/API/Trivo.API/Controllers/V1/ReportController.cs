using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trivo.Application.Features.Reports.Commands.CreateReport;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Reports;

namespace Trivo.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/report")]
public class ReportController(ISender sender) : ControllerBase
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ResultT<ReportDto>> CreateReportAsync(
        [FromBody] CreateReportCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }
}
