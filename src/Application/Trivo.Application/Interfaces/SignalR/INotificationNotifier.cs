
using Trivo.Application.DTOs.Notifications;

namespace Trivo.Application.Interfaces.SignalR;

public interface INotificationNotifier
{
    Task NotifyNewNotificationAsync(Guid userId, NotificationDto notification);

    Task NotifyNotificationsAsync(Guid userId, IEnumerable<NotificationDto> notifications);

    Task NotifyNotificationMarkedAsReadAsync(Guid userId, Guid notificationId, NotificationDto notification);

    Task NotifyNotificationDeletedAsync(Guid userId, Guid notificationId, NotificationDto notification);
}