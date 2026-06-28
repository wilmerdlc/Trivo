using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.Chat;

namespace Trivo.Application.Features.Chat.Query.GetChatPagination;

public sealed record GetChatPaginationQuery(
    Guid UserId,
    int PageNumber,
    int PageSize
) : IQuery<PagedResult<ChatDto>>;