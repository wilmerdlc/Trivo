using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Chat;

namespace Trivo.Application.Features.Messages.Query.GetMessagePagination;

internal sealed class GetMessagePaginationQueryHandler(
    ILogger<GetMessagePaginationQueryHandler> logger,
    IMessageRepository messageRepository,
    IChatRepository chatRepository,
    IRealTimeNotifier notifier
) : IQueryHandler<GetMessagePaginationQuery, PagedResult<MessageDto>>
{
    public async Task<ResultT<PagedResult<MessageDto>>> Handle(GetMessagePaginationQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            logger.LogWarning("The request to get paginated messages was null.");
            return ResultT<PagedResult<MessageDto>>.Failure(Error.Failure("400", "Request cannot be null."));
        }

        var pagedResult = await messageRepository.GetPagedByChatIdAsync(
            request.ChatId,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        var items = pagedResult.Items?.ToList() ?? [];

        if (!items.Any())
        {
            logger.LogWarning("No messages found on page {PageNumber}.", request.PageNumber);

            return ResultT<PagedResult<MessageDto>>.Failure(
                Error.Failure("404", "No messages found."));
        }

        var result = new PagedResult<MessageDto>(
            items: items,
            totalItems: pagedResult.TotalItems,
            currentPage: request.PageNumber,
            pageSize: request.PageSize
        );

        var chat = await chatRepository.GetChatWithUsersAndMessagesAsync(request.ChatId, cancellationToken);
        if (chat is null)
        {
            logger.LogWarning("Chat not found for message pagination notification.");
            return ResultT<PagedResult<MessageDto>>.Failure(Error.Failure("404", "Chat not found."));
        }

        var userIds = chat.ChatUsers!
            .Where(cu => cu.UserId.HasValue)
            .Select(cu => cu.UserId!.Value)
            .ToList();

        foreach (var userId in userIds)
        {
            await notifier.NotifyMessagePageAsync(userId, request.ChatId, result.Items!);
        }

        logger.LogInformation(
            "Successfully retrieved page {PageNumber} of messages. Total messages on this page: {Count}",
            request.PageNumber, items.Count);

        return ResultT<PagedResult<MessageDto>>.Success(result);
    }
}
