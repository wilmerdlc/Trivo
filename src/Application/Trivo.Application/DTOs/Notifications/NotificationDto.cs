using Trivo.Application.DTOs.Notifications;

namespace Trivo.Application.DTOs.Notifications;

public record NotificationDto(
    Guid NotificationId,
    Guid UserId,
    string? Type,
    string? Content,
    bool? IsRead,
    DateTime? CreatedAt,
    DateTime? ReadAt
);