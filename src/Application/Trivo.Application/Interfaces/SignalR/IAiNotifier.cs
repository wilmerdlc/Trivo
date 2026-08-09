
using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Interfaces.SignalR;

public interface IAiNotifier
{
    Task NotifyRecommendationsAsync(Guid userId, IEnumerable<UserAiRecommendationDto>? recommendations);

    Task NotifyNewRecommendationsAsync(Guid userId, IEnumerable<UserAiRecommendationDto>? recommendations);
}