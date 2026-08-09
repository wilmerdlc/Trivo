using Microsoft.Extensions.Logging;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Features.Notifications;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Notifications;

namespace Trivo.Application.Services;

public class NotificationService(
    INotificationNotifier notificationNotifier,
    ILogger<NotificationService> logger,
    IUserRepository userRepository,
    INotificationRepository notificationRepository,
    IUnitOfWork unitOfWork
) : INotificationService
{
    public async Task<ResultT<PagedResult<NotificationDto>>> GetNotificationsAsync(Guid userId,
        int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            logger.LogWarning("Invalid pagination parameters. Page: {Page}, PageSize: {PageSize}",
                pageNumber, pageSize);

            return ResultT<PagedResult<NotificationDto>>.Failure(
                Error.Failure("400", "Pagination parameters must be greater than zero"));
        }

        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("User with ID {UserId} not found when trying to get notifications", userId);
            return ResultT<PagedResult<NotificationDto>>.Failure(
                Error.NotFound("404", $"User with ID {userId} not found"));
        }

        // Nomenclatura de repositorio mejorada: GetPagedByUserIdAsync
        var notifications = await notificationRepository.GetPagedByUserIdAsync(userId,
            pageNumber,
            pageSize,
            cancellationToken);

        if (!notifications.Items!.Any())
        {
            logger.LogWarning("No notifications found for user {UserId}", userId);
            return ResultT<PagedResult<NotificationDto>>.Failure(Error.Failure("400", "The list is empty"));
        }

        var notificationsDto = NotificationMapper.MapToDtoList(notifications.Items!);

        logger.LogInformation("Retrieved {Count} notifications for user {UserId}",
            notificationsDto.Count, userId);

        PagedResult<NotificationDto> pagedResult = new
        (
            items: notificationsDto,
            totalItems: notifications.TotalItems,
            currentPage: notifications.CurrentPage,
            pageSize: pageSize
        );

        await notificationNotifier.NotifyNotificationsAsync(userId, pagedResult.Items!);

        logger.LogInformation("Notifications sent via SignalR. Count: {Count}",
            notificationsDto.Count);

        return ResultT<PagedResult<NotificationDto>>.Success(pagedResult);
    }

    public async Task<ResultT<NotificationDto>> MarkAsReadAsync(Guid notificationId, Guid userId,
        CancellationToken cancellationToken)
    {
        if (notificationId == Guid.Empty)
        {
            logger.LogWarning("Attempted to mark notification with empty ID");
            return ResultT<NotificationDto>.Failure(
                Error.Failure("400", "Notification ID cannot be empty"));
        }

        var notificationEntity = await notificationRepository.GetByIdAndUserIdAsync(
            notificationId,
            userId,
            cancellationToken);

        if (notificationEntity is null)
        {
            logger.LogWarning("Notification {NotificationId} not found for user {UserId}",
                notificationId, userId);

            return ResultT<NotificationDto>.Failure(
                Error.NotFound("404", "Notification not found or does not belong to the user"));
        }

        if (notificationEntity.IsRead.GetValueOrDefault())
        {
            logger.LogInformation("Notification {NotificationId} was already marked as read", notificationId);
            return ResultT<NotificationDto>.Success(
                NotificationMapper.MapToDto(notificationEntity));
        }

        notificationEntity.IsRead = true;
        notificationEntity.ReadAt = DateTime.UtcNow;

        await notificationRepository.UpdateAsync(notificationEntity, cancellationToken);

        var notificationDto = NotificationMapper.MapToDto(notificationEntity);

        await notificationNotifier.NotifyNotificationMarkedAsReadAsync(userId, notificationId, notificationDto);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Notification {NotificationId} updated to 'read' for user {UserId}",
            notificationId, userId);

        return ResultT<NotificationDto>.Success(notificationDto);
    }

    public async Task<ResultT<NotificationDto>> CreateNotificationByTypeAsync(Guid userId,
        string? notificationType,
        string? content,
        CancellationToken cancellationToken)
    {
        var createDto = new CreateNotificationDto(
            UserId: userId,
            NotificationType: notificationType,
            Content: content
        );

        if (userId == Guid.Empty)
        {
            logger.LogWarning("Attempted to create notification with empty UserId");
            return ResultT<NotificationDto>.Failure(Error.Failure("400", "UserId cannot be empty"));
        }

        if (string.IsNullOrEmpty(notificationType))
        {
            logger.LogWarning("Notification type is empty");
            return ResultT<NotificationDto>.Failure(Error.Failure("400",
                "Notification type cannot be empty"));
        }

        return await CreateInternalNotificationAsync(createDto, cancellationToken);
    }

    public async Task<ResultT<NotificationDto>> DeleteNotificationAsync(Guid notificationId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var notification = await notificationRepository.GetByIdAsync(notificationId, cancellationToken);

        if (notification is null)
        {
            logger.LogWarning("Notification with ID {NotificationId} does not exist", notificationId);
            return ResultT<NotificationDto>.Failure(Error.NotFound("404",
                $"Notification with ID {notificationId} not found"));
        }

        if (userId == Guid.Empty)
        {
            logger.LogWarning("Attempted to delete notification with empty UserId");
            return ResultT<NotificationDto>.Failure(Error.Failure("400", "UserId cannot be empty"));
        }

        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("User with ID {UserId} not found", userId);
            return ResultT<NotificationDto>.Failure(Error.NotFound("404",
                $"User with ID {userId} not found"));
        }

        await notificationRepository.DeleteAsync(notification, cancellationToken);

        var notificationDto = NotificationMapper.MapToDto(notification);

        await notificationNotifier.NotifyNotificationDeletedAsync(userId, notificationId, notificationDto);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Notification {NotificationId} successfully deleted for user {UserId}",
            notificationId, userId);

        return ResultT<NotificationDto>.Success(notificationDto);
    }

    #region Private Methods

    private async Task<ResultT<NotificationDto>> CreateInternalNotificationAsync(CreateNotificationDto notificationDto,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(notificationDto.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("User with ID {UserId} not found when trying to create notification",
                notificationDto.UserId);
            return ResultT<NotificationDto>.Failure(Error.NotFound("404", "User not found"));
        }

        if (string.IsNullOrEmpty(notificationDto.Content))
        {
            logger.LogWarning("Attempted to create notification without content for user {UserId}",
                notificationDto.UserId);
            return ResultT<NotificationDto>.Failure(Error.Failure("400",
                "Notification content cannot be empty"));
        }

        var notificationEntity = NotificationMapper.MapToEntity(notificationDto);

        await notificationRepository.CreateAsync(notificationEntity, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "Notification successfully created - ID: {NotificationId}, Type: {Type}, User: {UserId}",
            notificationEntity.NotificationId, notificationEntity.Type, notificationEntity.UserId);

        var resultDto = NotificationMapper.MapToDto(notificationEntity);

        await NotifyAndLogAsync(notificationDto.UserId, resultDto);

        return ResultT<NotificationDto>.Success(resultDto);
    }

    private async Task NotifyAndLogAsync(Guid userId, NotificationDto notificationDto)
    {
        try
        {
            await notificationNotifier.NotifyNewNotificationAsync(userId, notificationDto);
            logger.LogInformation(
                "Notification sent in real-time - User: {UserId}, Notification: {NotificationId}",
                userId, notificationDto.NotificationId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error notifying user {UserId} via SignalR", userId);
        }
    }

    #endregion
}