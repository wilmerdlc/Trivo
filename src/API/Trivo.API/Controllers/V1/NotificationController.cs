using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Notifications;

namespace Trivo.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/notifications")]
public class NotificationController(
    INotificationService notificationService
) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<ResultT<NotificationDto>> CreateMatchNotificationAsync(
        [FromBody] CreateNotificationDto notificationParameter,
        CancellationToken cancellationToken)
    {
        return await notificationService.CreateNotificationByTypeAsync(
            notificationParameter.UserId,
            notificationParameter.NotificationType,
            notificationParameter.Content,
            cancellationToken);
    }

    [HttpDelete("{notificationId}/users/{userId}")]
    public async Task<ResultT<NotificationDto>> DeleteNotificationAsync(
        [FromRoute] Guid notificationId,
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        return await notificationService.DeleteNotificationAsync(notificationId, userId, cancellationToken);
    }

    [HttpPut("{notificationId}/users/{userId}")]
    [Authorize]
    public async Task<ResultT<NotificationDto>> MarkNotificationAsReadAsync(
        [FromRoute] Guid notificationId,
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        return await notificationService.MarkAsReadAsync(notificationId, userId, cancellationToken);
    }
}
