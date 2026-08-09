using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.Interests;

namespace Trivo.Application.Features.Interests.Query.GetInterestsByCategoryId;

public sealed record GetInterestsByCategoryIdQuery(
    IEnumerable<Guid> CategoryIds,
    int PageNumber,
    int PageSize
) : IQuery<PagedResult<InterestByCategoryIdDto>>;