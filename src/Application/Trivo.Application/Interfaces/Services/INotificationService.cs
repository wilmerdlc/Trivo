using Trivo.Application.Pagination;
using Trivo.Application.Utils;


using Trivo.Application.DTOs.Notifications;

namespace Trivo.Application.Interfaces.Services;

public interface INotificationService
{
    Task<ResultT<PagedResult<NotificationDto>>> GetNotificationsAsync(
        Guid userId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);

    Task<ResultT<NotificationDto>> MarkAsReadAsync(
        Guid notificationId, 
        Guid userId,
        CancellationToken cancellationToken);

    Task<ResultT<NotificationDto>> DeleteNotificationAsync(
        Guid notificationId,
        Guid userId,
        CancellationToken cancellationToken);

    Task<ResultT<NotificationDto>> CreateNotificationByTypeAsync(
        Guid userId,
        string? notificationType,
        string? content,
        CancellationToken cancellationToken);
}