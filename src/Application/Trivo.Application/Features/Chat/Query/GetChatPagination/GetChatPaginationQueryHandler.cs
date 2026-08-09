using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Chat;

namespace Trivo.Application.Features.Chat.Query.GetChatPagination;

internal sealed class GetChatPaginationQueryHandler(
    ILogger<GetChatPaginationQueryHandler> logger,
    IChatRepository chatRepository,
    IRealTimeNotifier notifier
) : IQueryHandler<GetChatPaginationQuery, PagedResult<ChatDto>>
{
    public async Task<ResultT<PagedResult<ChatDto>>> Handle(GetChatPaginationQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            logger.LogWarning("The request to get paginated chats was null.");
            return ResultT<PagedResult<ChatDto>>.Failure(Error.Failure("400", "Request cannot be null."));
        }

        if (request.PageNumber <= 0 || request.PageSize <= 0)
        {
            logger.LogWarning("Invalid pagination parameters. PageNumber={PageNumber}, PageSize={PageSize}", 
                request.PageNumber, request.PageSize);
            
            return ResultT<PagedResult<ChatDto>>.Failure(Error.Failure("400", "Pagination parameters must be greater than zero."));
        }

        var pagedResult = await chatRepository.GetChatsByUserIdPagedAsync(
            request.UserId,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        var items = pagedResult.Items!
            .Select(chat => chat.ToDto(request.UserId)) 
            .ToList();
        
        if (!items.Any())
        {
            logger.LogWarning("No chats found on page {PageNumber}.", request.PageNumber);
            
            return ResultT<PagedResult<ChatDto>>.Failure(
                Error.Failure("404", "No chats found."));
        }
        
        var result = new PagedResult<ChatDto>(
            items: items,
            totalItems: pagedResult.TotalItems,
            currentPage: request.PageNumber,
            pageSize: request.PageSize
        );
        
        await notifier.NotifyPagedChatsAsync(request.UserId, result.Items!);

        logger.LogInformation(
            "Successfully retrieved page {PageNumber} of chats. Total chats on this page: {Count}",
            request.PageNumber, items.Count);

        return ResultT<PagedResult<ChatDto>>.Success(result);
    }
}