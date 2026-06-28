
using Trivo.Application.DTOs.Matching;

namespace Trivo.Application.Interfaces.SignalR;

public interface IMatchNotifier
{
    Task NotifyMatchAsync(Guid recruiterId, Guid expertId,
        IEnumerable<MatchDto> recruiterMatches,
        IEnumerable<MatchDto> expertMatches);

    Task NotifyNewMatchAsync(Guid recruiterId, Guid expertId,
        IEnumerable<MatchDto> recruiterMatches,
        IEnumerable<MatchDto> expertMatches);

    Task NotifyMatchCompletedAsync(Guid userId,
        Guid matchId,
        MatchDetailsDto matchDetailsDto);

    Task NotifyMatchRejectedAsync(Guid userId,
        Guid matchId,
        MatchDetailsDto matchDetailsDto);
}