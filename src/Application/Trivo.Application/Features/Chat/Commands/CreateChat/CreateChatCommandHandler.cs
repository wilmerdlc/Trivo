using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Chat;

namespace Trivo.Application.Features.Chat.Commands.CreateChat;

internal sealed class CreateChatCommandHandler(
    ILogger<CreateChatCommandHandler> logger,
    IChatRepository chatRepository,
    IUserRepository userRepository,
    IRealTimeNotifier notifier,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateChatCommand, ChatDto>
{
    public async Task<ResultT<ChatDto>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            logger.LogWarning("The CreateChatCommand request is null.");
            return ResultT<ChatDto>.Failure(Error.NotFound("404", "The command cannot be null"));
        }

        if (request.SenderId == request.ReceiverId)
        {
            return ResultT<ChatDto>.Failure(Error.Failure("400", "Sender and receiver cannot be the same user."));
        }

        var existingChat = await chatRepository.FindOneToOneChatAsync(request.SenderId, request.ReceiverId, cancellationToken);
        if (existingChat is not null)
        {
            logger.LogWarning("Chat already exists between {SenderId} and {ReceiverId}", request.SenderId, request.ReceiverId);
            return ResultT<ChatDto>.Failure(Error.Failure("400", "A chat already exists between these users."));
        }

        var sender = await userRepository.GetByIdAsync(request.SenderId, cancellationToken);
        var receiver = await userRepository.GetByIdAsync(request.ReceiverId, cancellationToken);

        if (sender is null || receiver is null)
        {
            logger.LogWarning("One or both users were not found. Sender: {SenderId}, Receiver: {ReceiverId}", request.SenderId, request.ReceiverId);
            return ResultT<ChatDto>.Failure(Error.NotFound("404", "One or both users were not found."));
        }

        Domain.Models.Chat newChat = request.ToEntity(sender, receiver);

        await chatRepository.CreateAsync(newChat, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var result = newChat.ToDto(request.SenderId);

        await notifier.NotifyNewChatAsync(request.SenderId, [result]);
        await notifier.NotifyNewChatAsync(request.ReceiverId, [result]);

        logger.LogInformation("Successfully created new chat with ID {ChatId} between {SenderId} and {ReceiverId}", 
            newChat.Id, request.SenderId, request.ReceiverId);

        return ResultT<ChatDto>.Success(result);
    }
}