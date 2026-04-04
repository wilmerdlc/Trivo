using Trivo.Application.Abstractions.Messages;
using Trivo.Application.DTOs.Interests;
using Trivo.Application.Pagination;

namespace Trivo.Application.Features.Interests.Query.GetInterestsByCategoryId;

public sealed record GetInterestsByCategoryIdQuery(
    IEnumerable<Guid> CategoryIds,
    int PageNumber,
    int PageSize
) : IQuery<PagedResult<InterestByCategoryIdDto>>;