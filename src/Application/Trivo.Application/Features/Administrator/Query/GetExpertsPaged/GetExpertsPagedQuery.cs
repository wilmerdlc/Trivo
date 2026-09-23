using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Query.GetExpertsPaged;

public sealed record GetExpertsPagedQuery(int PageNumber, int PageSize)
    : IQuery<PagedResult<AdminExpertDto>>;
