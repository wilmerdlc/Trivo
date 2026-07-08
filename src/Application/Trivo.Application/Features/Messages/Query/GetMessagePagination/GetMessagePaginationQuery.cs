using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.Chat;

namespace Trivo.Application.Features.Messages.Query.GetMessagePagination;

public sealed record GetMessagePaginationQuery(
    Guid ChatId,
    int PageNumber,
    int PageSize
) : IQuery<PagedResult<MessageDto>>;
