using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Trivo.Application.DTOs;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.SignalR;

namespace Trivo.Infrastructure.Shared.SignalR.Hubs;

[Authorize]
public class NotificationHub(
    ILogger<NotificationHub> logger,
    INotificationService notificationService
) : Hub<INotificationHub>
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public async Task MarkAsRead(Guid notificationId)
    {
        try
        {
            if (!Guid.TryParse(Context.UserIdentifier, out var userId))
            {
                logger.LogWarning("Attempt to mark notification with invalid UserIdentifier: {UserIdentifier}",
                    Context.UserIdentifier);
                return;
            }

            logger.LogInformation(
                "Request to mark notification {NotificationId} as read by user {UserId}",
                notificationId, userId);

            var result =
                await notificationService.MarkAsReadAsync(notificationId, userId, CancellationToken.None);

            if (!result.IsSuccess)
            {
                logger.LogWarning("Error marking notification {NotificationId} as read: {Error}",
                    notificationId, result.Error);
                return;
            }

            await Clients.User(userId.ToString()).ReceiveNotificationMarkedAsReadAsync(notificationId, result.Value);

            logger.LogInformation("Notification {NotificationId} marked as read successfully", notificationId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error marking notification {NotificationId} as read", notificationId);
        }
    }

    public async Task DeleteNotification(Guid notificationId)
    {
        try
        {
            if (!Guid.TryParse(Context.UserIdentifier, out var userId))
            {
                logger.LogWarning("Attempt to delete notification with invalid UserIdentifier: {UserIdentifier}",
                    Context.UserIdentifier);
                return;
            }

            logger.LogInformation("User {UserId} is attempting to delete notification {NotificationId}",
                userId, notificationId);

            var result =
                await notificationService.DeleteNotificationAsync(notificationId, userId, CancellationToken.None);
            
            if (!result.IsSuccess)
            {
                logger.LogWarning("Failed to delete notification {NotificationId} for user {UserId}",
                    notificationId, userId);
                return;
            }

            await Clients.User(userId.ToString()).ReceiveNotificationDeletedAsync(notificationId, result.Value);

            logger.LogInformation("Notification {NotificationId} deleted successfully by user {UserId}",
                notificationId, userId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error deleting notification {NotificationId}", notificationId);
        }
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        logger.LogInformation("User disconnected: {UserIdentifier}", Context.UserIdentifier);
        return base.OnDisconnectedAsync(exception);
    }

    public async Task GetNotifications(int pageNumber = 1, int pageSize = 20)
    {
        var userIdString = Context.UserIdentifier;

        if (!Guid.TryParse(userIdString, out var userId))
        {
            logger.LogError("UserIdentifier is not a valid GUID");
            return;
        }

        logger.LogInformation("User connected:");
        logger.LogInformation("- UserIdentifier (SignalR): {UserIdentifier}", userIdString);

        var result = await notificationService.GetNotificationsAsync(userId, pageNumber, pageSize, CancellationToken.None);
        
        if (!result.IsSuccess)
        {
            logger.LogWarning("Error getting notifications for user {UserId}: {Error}",
                userId, result.Error);
            return;
        }

        await Clients.Caller.ReceiveNotificationsAsync(result.Value.Items!);
    }
}