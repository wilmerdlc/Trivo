using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Trivo.Application.Features.Chat.Query.GetChatPagination;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.SignalR;

using Trivo.Application.DTOs.Chat;

namespace Trivo.Infrastructure.Shared.SignalR.Hubs;

[Authorize]
public class ChatHub(
    ILogger<ChatHub> logger,
    IMediator mediator,
    IRealTimeNotifier realTimeNotifier,
    IChatRepository chatRepository
) : Hub<IChatHub>
{
    public override async Task OnConnectedAsync()
    {
        if (!Guid.TryParse(Context.UserIdentifier, out var userId))
        {
            logger.LogWarning("UserIdentifier is not a valid GUID: {UserIdentifier}", Context.UserIdentifier);
            return;
        }

        logger.LogInformation("User connected: {UserId}", userId);

        await mediator.Send(new GetChatPaginationQuery(
            userId,
            PageNumber: 1,
            PageSize: 9
        ));

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;
        logger.LogInformation("User disconnected: {UserId}", userId);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(MessageDto message, CancellationToken cancellationToken = default)
    {
        var senderId = Context.UserIdentifier;

        if (!Guid.TryParse(senderId, out var senderGuid))
        {
            logger.LogWarning("UserIdentifier is not a valid GUID");
            return;
        }

        // This only relays a real-time notification — the message itself is persisted via the
        // REST/SendMessageCommand path, which already does this same check. Without it here too,
        // any connected client could push a fabricated "message" to an arbitrary ReceiverId for a
        // ChatId they have nothing to do with.
        var chatExists = await chatRepository.ExistsAsync(message.ChatId, cancellationToken);
        var senderBelongs = chatExists && await chatRepository.IsUserInChatAsync(message.ChatId, senderGuid, cancellationToken);
        var receiverBelongs = chatExists && await chatRepository.IsUserInChatAsync(message.ChatId, message.ReceiverId, cancellationToken);

        if (!senderBelongs || !receiverBelongs)
        {
            logger.LogWarning(
                "Rejected SendMessage: sender {SenderId} or receiver {ReceiverId} does not belong to chat {ChatId}",
                senderGuid, message.ReceiverId, message.ChatId);
            return;
        }

        logger.LogInformation("User {SenderId} sends message to {ReceiverId}: {Content}",
            senderId, message.ReceiverId, message.Content);

        await Clients.User(message.ReceiverId.ToString())
            .ReceivePrivateMessage(message with { SenderId = senderGuid });

        await Clients.User(senderGuid.ToString())
            .ReceivePrivateMessage(message with { SenderId = senderGuid });
    }

    public async Task GetChatMessages(Guid chatId, int pageNumber = 1, int pageSize = 20)
    {
        // await mediator.Send(new GetMessagePagesQuery(
        //     chatId,
        //     pageNumber,
        //     pageSize
        // ));
    }
}