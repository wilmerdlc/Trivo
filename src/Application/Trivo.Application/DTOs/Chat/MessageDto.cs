using Trivo.Application.DTOs.Chat;

namespace Trivo.Application.DTOs.Chat;

public sealed record MessageDto(
    Guid MessageId,
    Guid ChatId,
    string? Content,
    string? Status,
    DateTime? SentDate,
    Guid SenderId,
    Guid ReceiverId,
    string? MessageType
);