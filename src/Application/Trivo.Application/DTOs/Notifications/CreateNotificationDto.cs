using Trivo.Application.DTOs.Notifications;

namespace Trivo.Application.DTOs.Notifications;

public sealed record CreateNotificationDto(
    Guid UserId,
    string? NotificationType,
    string? Content
);