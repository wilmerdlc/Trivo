using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trivo.Application.Features.InterestCategories.Commands.CreateInterestCategory;
using Trivo.Application.Features.InterestCategories.Query.GetPaginatedInterestCategories;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.InterestCategories;

namespace Trivo.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/category-interests")]
public class InterestCategoryController(ISender sender) : ControllerBase
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ResultT<InterestCategoryDto>> CreateInterestCategoryAsync(
        [FromBody] CreateInterestCategoryCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpGet("pagination")]
    public async Task<ResultT<PagedResult<InterestCategoryDto>>> GetPaginatedAsync(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetPaginatedInterestCategoriesQuery(pageNumber, pageSize), cancellationToken);
    }
}
