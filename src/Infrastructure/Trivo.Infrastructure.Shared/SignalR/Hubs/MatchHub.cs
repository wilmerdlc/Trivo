using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Trivo.Application.Features.Matching.Query.GetMatchByUser;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Domain.Enums;

using Trivo.Application.DTOs.Matching;

namespace Trivo.Infrastructure.Shared.SignalR.Hubs;

[Authorize]
public class MatchHub(
    ILogger<MatchHub> logger,
    IMediator mediator
) : Hub<IMatchHub>
{
    public override async Task OnConnectedAsync()
    {
        try
        {
            var userIdentifier = Context.UserIdentifier;
            var httpContext = Context.GetHttpContext();
            var query = httpContext?.Request.Query;

            var pageNumberString = query?["pageNumber"];
            var pageSizeString = query?["pageSize"];

            var pageNumber = int.TryParse(pageNumberString, out var pn) ? pn : 1;
            var pageSize = int.TryParse(pageSizeString, out var ps) ? ps : 5;

            logger.LogInformation("User connected:");
            logger.LogInformation("- UserIdentifier (SignalR): {UserIdentifier}", userIdentifier);

            if (!Guid.TryParse(userIdentifier, out var userId))
            {
                logger.LogError("UserIdentifier is not a valid GUID");
                return;
            }

            logger.LogInformation("- UserId: {UserId}", userId);

            var claims = Context.User?.Claims.ToList();

            if (claims == null || !claims.Any())
            {
                logger.LogWarning("No claims found for the user.");
                return;
            }

            foreach (var claim in claims)
            {
                logger.LogInformation("Claim: {Type} = {Value}", claim.Type, claim.Value);
            }

            var rolesClaims = claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            if (rolesClaims.Count == 0)
            {
                logger.LogWarning("No role claims found.");
                return;
            }

            Roles role = default;
            if (!rolesClaims.Any(rc => Enum.TryParse(rc, ignoreCase: true, out role)))
            {
                logger.LogWarning("None of the roles in the token are valid. Roles found: {Roles}",
                    string.Join(", ", rolesClaims));
                return;
            }

            logger.LogInformation("User with valid role connected: {UserId} - Role: {Role}", 
                userId, role.ToString());

            var result = await mediator.Send(new GetMatchByUserQuery
            (
                userId,
                pageNumber,
                pageSize,
                role
            ));

            if (!result.IsSuccess)
            {
                logger.LogWarning("No matches found for user {UserId} with role {Role}.",
                    userId, role);

                await Clients.User(userId.ToString()).ReceiveMatchesAsync(new List<MatchDto>());
                await base.OnConnectedAsync();
                return;
            }

            await Clients.User(userId.ToString()).ReceiveMatchesAsync(result.Value);

            await base.OnConnectedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in OnConnectedAsync for SignalR user.");
            throw;
        }
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        logger.LogInformation("User disconnected: {UserIdentifier}", Context.UserIdentifier);
        return base.OnDisconnectedAsync(exception);
    }
}