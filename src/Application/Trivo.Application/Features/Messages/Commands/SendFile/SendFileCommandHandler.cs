using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Chat;

namespace Trivo.Application.Features.Messages.Commands.SendFile;

internal sealed class SendFileCommandHandler(
    ILogger<SendFileCommandHandler> logger,
    ICloudinaryService cloudinaryService,
    IChatRepository chatRepository,
    IMessageRepository messageRepository,
    IRealTimeNotifier notifier
) : ICommandHandler<SendFileCommand, MessageDto>
{
    public async Task<ResultT<MessageDto>> Handle(SendFileCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            logger.LogWarning("The request cannot be null.");
            return ResultT<MessageDto>.Failure(Error.Failure("400", "The request cannot be null."));
        }

        var chatExists = await chatRepository.ExistsAsync(request.ChatId, cancellationToken);

        if (!chatExists)
        {
            logger.LogWarning("Chat does not exist: {ChatId}", request.ChatId);
            return ResultT<MessageDto>.Failure(Error.Failure("404", "The chat does not exist."));
        }

        var senderBelongs = await chatRepository.IsUserInChatAsync(request.ChatId, request.SenderId, cancellationToken);
        var receiverBelongs = await chatRepository.IsUserInChatAsync(request.ChatId, request.ReceiverId, cancellationToken);

        if (!senderBelongs || !receiverBelongs)
        {
            logger.LogWarning("Sender or receiver does not belong to chat {ChatId}", request.ChatId);
            return ResultT<MessageDto>.Failure(Error.Failure("403", "Sender or receiver does not belong to the chat."));
        }

        var sender = await chatRepository.GetUserByIdAsync(request.SenderId, cancellationToken);
        var receiver = await chatRepository.GetUserByIdAsync(request.ReceiverId, cancellationToken);

        if (sender == null || receiver == null)
        {
            logger.LogWarning("Sender or receiver not found.");
            return ResultT<MessageDto>.Failure(Error.Failure("404", "Sender or receiver not found."));
        }

        string url = "";
        if (request.File != null)
        {
            using var stream = request.File.OpenReadStream();
            url = await cloudinaryService.UploadFileAsync(
                stream,
                request.File.FileName,
                cancellationToken);
        }

        logger.LogInformation("File uploaded successfully to Cloudinary. URL: {Url}", url);

        var message = new Message
        {
            MessageId = Guid.NewGuid(),
            ChatId = request.ChatId,
            SenderId = request.SenderId,
            ReceiverId = request.ReceiverId,
            Content = url,
            SentAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            Status = MessageStatus.Sent.ToString(),
            Type = MessageType.File.ToString()
        };

        await messageRepository.CreateAsync(message, cancellationToken);

        logger.LogInformation("Message persisted in the database. Id: {MessageId}", message.MessageId);

        var dto = new MessageDto(
            message.MessageId!.Value,
            message.ChatId!.Value,
            message.Content,
            message.Status,
            message.SentAt ?? DateTime.UtcNow,
            message.SenderId!.Value,
            message.ReceiverId,
            message.Type
        );

        await notifier.NotifyPrivateMessageAsync(dto, dto.SenderId);
        await notifier.NotifyPrivateMessageAsync(dto, dto.ReceiverId);

        logger.LogInformation("Message sent from {SenderId} to {ReceiverId}", request.SenderId, request.ReceiverId);

        return ResultT<MessageDto>.Success(dto);
    }
}
