using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator.Query.GetRecruitersPaged;

public sealed record GetRecruitersPagedQuery(int PageNumber, int PageSize)
    : IQuery<PagedResult<AdminRecruiterDto>>;
