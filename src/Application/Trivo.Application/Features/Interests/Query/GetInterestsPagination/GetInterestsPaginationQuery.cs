using Trivo.Application.Abstractions.Messages;
using Trivo.Application.DTOs.Interests;
using Trivo.Application.Pagination;

namespace Trivo.Application.Features.Interests.Query.GetInterestsPagination;

public sealed record GetInterestsPaginationQuery(
    int PageNumber,
    int PageSize
) : IQuery<PagedResult<InterestDto>>;