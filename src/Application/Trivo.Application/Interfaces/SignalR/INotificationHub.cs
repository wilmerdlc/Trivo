
using Trivo.Application.DTOs.Notifications;

namespace Trivo.Application.Interfaces.SignalR;

public interface INotificationHub
{
    Task ReceiveNewNotificationAsync(NotificationDto notification);

    Task ReceiveNotificationsAsync(IEnumerable<NotificationDto> notifications);

    Task ReceiveNotificationMarkedAsReadAsync(Guid notificationId, NotificationDto notification);

    Task ReceiveNotificationDeletedAsync(Guid notificationId, NotificationDto notification);
}