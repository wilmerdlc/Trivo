using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Trivo.Application.DTOs.Users;
using Trivo.Application.Features.Users.Query.GetUserRecommendations;
using Trivo.Application.Interfaces.SignalR;

namespace Trivo.Infrastructure.Shared.SignalR.Hubs;

[Authorize]
public class UserRecommendationHub(
    ILogger<UserRecommendationHub> logger,
    IMediator mediator
) : Hub<IUserRecommendationHub>
{
    public override async Task OnConnectedAsync()
    {
        var userIdentifier = Context.UserIdentifier;

        logger.LogInformation("🔌 User connected to recommendations hub:");
        logger.LogInformation("- UserIdentifier (SignalR): {UserIdentifier}", userIdentifier);

        if (!Guid.TryParse(userIdentifier, out var userId))
        {
            logger.LogError("UserIdentifier is not a valid GUID");
            await base.OnConnectedAsync();
            return;
        }

        var httpContext = Context.GetHttpContext();
        var query = httpContext?.Request.Query;

        var pageNumberString = query?["pageNumber"];
        var pageSizeString = query?["pageSize"];

        var pageNumber = int.TryParse(pageNumberString, out var pn) ? pn : 1;
        var pageSize = int.TryParse(pageSizeString, out var ps) ? ps : 5;

        logger.LogInformation("- UserId: {UserId}", userId);

        var result = await mediator.Send(new GetUserRecommendationsQuery(userId, pageNumber, pageSize));

        if (!result.IsSuccess)
        {
            logger.LogWarning("No recommendations found for user {UserId}. Error: {Error}", userId, result.Error);
            await Clients.Caller.ReceiveRecommendationsAsync([]);
            await base.OnConnectedAsync();
            return;
        }

        await Clients.Caller.ReceiveRecommendationsAsync(result.Value.Items ?? []);
        await base.OnConnectedAsync();
    }

    public async Task GetRecommendations(int pageNumber = 1, int pageSize = 5)
    {
        var userIdentifier = Context.UserIdentifier;

        if (!Guid.TryParse(userIdentifier, out var userId))
        {
            logger.LogError("UserIdentifier is not a valid GUID");
            return;
        }

        logger.LogInformation("Fetching recommendations for user {UserId} (Page: {PageNumber}, Size: {PageSize})",
            userId, pageNumber, pageSize);

        var result = await mediator.Send(new GetUserRecommendationsQuery(userId, pageNumber, pageSize));

        if (!result.IsSuccess)
        {
            logger.LogWarning("No recommendations found for user {UserId}. Error: {Error}", userId, result.Error);
            await Clients.Caller.ReceiveRecommendationsAsync([]);
            return;
        }

        await Clients.Caller.ReceiveRecommendationsAsync(result.Value.Items ?? []);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        logger.LogInformation("User disconnected from recommendations hub: {UserIdentifier}", Context.UserIdentifier);
        return base.OnDisconnectedAsync(exception);
    }
}