using Trivo.Application.Abstractions.Messages;
using Trivo.Application.DTOs.Match;
using Trivo.Domain.Enums;

namespace Trivo.Application.Features.Matching.Query.GetMatchByUser;

public sealed record GetMatchByUserQuery
(
    Guid UserId,
    int PageNumber,
    int PageSize,
    Roles Role
) : IQuery<IEnumerable<MatchDto>>;