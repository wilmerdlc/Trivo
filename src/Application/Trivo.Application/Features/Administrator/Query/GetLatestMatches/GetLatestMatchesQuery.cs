using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Query.GetLatestMatches;

public sealed record GetLatestMatchesQuery(
    int PageNumber,
    int PageSize
) : IQuery<PagedResult<AdminMatchDto>>;