using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.Skills;

namespace Trivo.Application.Features.Skills.Query.GetSkillsPagination;

public sealed record GetSkillsPaginationQuery(
    int PageNumber,
    int PageSize
) : IQuery<PagedResult<SkillDto>>;