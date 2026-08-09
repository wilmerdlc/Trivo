using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trivo.Application.Features.Skills.Commands.CreateSkill;
using Trivo.Application.Features.Skills.Query.GetSkillsPagination;
using Trivo.Application.Features.Skills.Query.SearchSkillsByName;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Skills;

namespace Trivo.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ability")]
public class SkillsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ResultT<SkillDto>> CreateSkillAsync(
        [FromBody] CreateSkillCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpGet("pagination")]
    public async Task<ResultT<PagedResult<SkillDto>>> GetPaginatedAsync(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetSkillsPaginationQuery(pageNumber, pageSize), cancellationToken);
    }

    [HttpGet("search")]
    public async Task<ResultT<IEnumerable<SkillWithIdDto>>> SearchByNameAsync(
        [FromQuery] string name,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new SearchSkillsByNameQuery(name), cancellationToken);
    }
}
