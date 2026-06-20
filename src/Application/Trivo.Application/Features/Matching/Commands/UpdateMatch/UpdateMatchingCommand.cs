using Trivo.Application.Abstractions.Messages;
using Trivo.Application.DTOs.Match;
using Trivo.Domain.Enums;

namespace Trivo.Application.Features.Matching.Commands.UpdateMatch;

public sealed record UpdateMatchingCommand
(
    Guid MatchingId,
    Guid UserId,
    MissingByMatching? MissingByMatching,
    MatchingUpdateStatus? Status
) : ICommand<MatchDetailsDto>;