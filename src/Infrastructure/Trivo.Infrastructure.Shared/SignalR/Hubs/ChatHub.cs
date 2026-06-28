using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Trivo.Application.Interfaces.SignalR;

using Trivo.Application.DTOs.Chat;

namespace Trivo.Infrastructure.Shared.SignalR.Hubs;

[Authorize]
public class ChatHub(
    ILogger<ChatHub> logger,
    IMediator mediator,
    IRealTimeNotifier realTimeNotifier
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

        // await mediator.Send(new GetChatPagesQuery(
        //     userId,
        //     PageNumber: 1,
        //     PageSize: 9
        // ));

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;
        logger.LogInformation("User disconnected: {UserId}", userId);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(MessageDto message)
    {
        var senderId = Context.UserIdentifier;

        if (!Guid.TryParse(senderId, out var senderGuid))
        {
            logger.LogWarning("UserIdentifier is not a valid GUID");
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