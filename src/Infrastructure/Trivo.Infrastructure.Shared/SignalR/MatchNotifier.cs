using Microsoft.AspNetCore.SignalR;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Infrastructure.Shared.SignalR.Hubs;

using Trivo.Application.DTOs.Matching;

namespace Trivo.Infrastructure.Shared.SignalR;

public class MatchNotifier(IHubContext<MatchHub, IMatchHub> hubContext)
    : IMatchNotifier
{
    public async Task NotifyMatchAsync(Guid recruiterId, Guid expertId,
        IEnumerable<MatchDto> recruiterMatches,
        IEnumerable<MatchDto> expertMatches)
    {
        if (recruiterId == expertId)
        {
            await hubContext.Clients.User(recruiterId.ToString())
                .ReceiveMatchesAsync(recruiterMatches);
        }
        else
        {
            await hubContext.Clients.User(recruiterId.ToString())
                .ReceiveMatchesAsync(recruiterMatches);

            await hubContext.Clients.User(expertId.ToString())
                .ReceiveMatchesAsync(expertMatches);
        }
    }

    public async Task NotifyNewMatchAsync(Guid recruiterId, Guid expertId,
        IEnumerable<MatchDto> recruiterMatches,
        IEnumerable<MatchDto> expertMatches)
    {
        if (recruiterId == expertId)
        {
            await hubContext.Clients.User(recruiterId.ToString())
                .ReceiveNewMatchesAsync(recruiterMatches);
        }
        else
        {
            await hubContext.Clients.User(recruiterId.ToString())
                .ReceiveNewMatchesAsync(recruiterMatches);

            await hubContext.Clients.User(expertId.ToString())
                .ReceiveNewMatchesAsync(expertMatches);
        }
    }

    public async Task NotifyMatchCompletedAsync(Guid userId, Guid matchId,
        MatchDetailsDto matchDetailsDto)
    {
        await hubContext.Clients.User(userId.ToString())
            .ReceiveMatchCompletedAsync(matchId, matchDetailsDto);
    }

    public async Task NotifyMatchRejectedAsync(Guid userId, Guid matchId,
        MatchDetailsDto matchDetailsDto)
    {
        await hubContext.Clients.User(userId.ToString())
            .ReceiveMatchRejectedAsync(matchId, matchDetailsDto);
    }
}