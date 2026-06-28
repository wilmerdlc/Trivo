using Microsoft.AspNetCore.SignalR;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Infrastructure.Shared.SignalR.Hubs;

using Trivo.Application.DTOs.Notifications;

namespace Trivo.Infrastructure.Shared.SignalR;

public class NotificationNotifier(
    IHubContext<NotificationHub, INotificationHub> hub
) : INotificationNotifier
{
    public async Task NotifyNewNotificationAsync(Guid userId, NotificationDto notification)
    {
        await hub.Clients.User(userId.ToString())
            .ReceiveNewNotificationAsync(notification);
    }

    public async Task NotifyNotificationsAsync(Guid userId, IEnumerable<NotificationDto> notifications)
    {
        await hub.Clients.User(userId.ToString())
            .ReceiveNotificationsAsync(notifications);
    }

    public async Task NotifyNotificationMarkedAsReadAsync(Guid userId, Guid notificationId,
        NotificationDto notification)
    {
        await hub.Clients.User(userId.ToString())
            .ReceiveNotificationMarkedAsReadAsync(notificationId, notification);
    }

    public async Task NotifyNotificationDeletedAsync(Guid userId, Guid notificationId, NotificationDto notification)
    {
        await hub.Clients.User(userId.ToString())
            .ReceiveNotificationDeletedAsync(notificationId, notification);
    }
}