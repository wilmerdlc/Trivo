using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trivo.Application.Features.Interests.Commands.CreateInterest;
using Trivo.Application.Features.Interests.Query.GetInterestsByCategoryId;
using Trivo.Application.Features.Interests.Query.GetInterestsPagination;
using Trivo.Application.Features.Interests.Query.SearchInterestsByNameInterest;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Interests;

namespace Trivo.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/interests")]
public class InterestController(ISender sender) : ControllerBase
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ResultT<InterestDetailsDto>> CreateInterestAsync(
        [FromBody] CreateInterestCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpGet("by-categories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ResultT<PagedResult<InterestByCategoryIdDto>>> GetByCategoriesAsync(
        [FromQuery] List<Guid> categoryIds,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetInterestsByCategoryIdQuery(categoryIds, pageNumber, pageSize), cancellationToken);
    }

    [HttpGet("pagination")]
    public async Task<ResultT<PagedResult<InterestDto>>> GetPaginatedAsync(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetInterestsPaginationQuery(pageNumber, pageSize), cancellationToken);
    }

    [HttpGet("search")]
    [Authorize]
    public async Task<ResultT<IEnumerable<InterestWithIdDto>>> SearchByNameAsync(
        [FromQuery] string name,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new SearchInterestsByNameQuery(name), cancellationToken);
    }
}
