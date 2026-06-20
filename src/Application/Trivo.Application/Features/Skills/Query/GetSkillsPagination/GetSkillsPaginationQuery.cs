using Trivo.Application.Abstractions.Messages;
using Trivo.Application.DTOs.Skills;
using Trivo.Application.Pagination;

namespace Trivo.Application.Features.Skills.Query.GetSkillsPagination;

public sealed record GetSkillsPaginationQuery(
    int PageNumber,
    int PageSize
) : IQuery<PagedResult<SkillDto>>;