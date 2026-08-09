using Trivo.Application.Abstractions.Messages;
using Trivo.Domain.Enums;

using Trivo.Application.DTOs.Matching;

namespace Trivo.Application.Features.Matching.Query.GetMatchByUser;

public sealed record GetMatchByUserQuery
(
    Guid UserId,
    int PageNumber,
    int PageSize,
    Roles Role
) : IQuery<IEnumerable<MatchDto>>;