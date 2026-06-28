
using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Interfaces.SignalR;

public interface IUserRecommendationHub
{
    Task ReceiveRecommendationsAsync(IEnumerable<UserAiRecommendationDto>? recommendations);

    Task NotifyNewRecommendationAsync(IEnumerable<UserAiRecommendationDto>? recommendations);
}