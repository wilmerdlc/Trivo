using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.Interests;

namespace Trivo.Application.Features.Interests.Query.GetInterestsPagination;

public sealed record GetInterestsPaginationQuery(
    int PageNumber,
    int PageSize
) : IQuery<PagedResult<InterestDto>>;