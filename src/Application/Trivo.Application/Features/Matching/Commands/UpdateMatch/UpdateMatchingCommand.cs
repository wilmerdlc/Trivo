using Trivo.Application.Abstractions.Messages;
using Trivo.Domain.Enums;

using Trivo.Application.DTOs.Matching;

namespace Trivo.Application.Features.Matching.Commands.UpdateMatch;

public sealed record UpdateMatchingCommand
(
    Guid MatchingId,
    Guid UserId,
    MissingByMatching? MissingByMatching,
    MatchUpdateStatus? Status
) : ICommand<MatchDetailsDto>;