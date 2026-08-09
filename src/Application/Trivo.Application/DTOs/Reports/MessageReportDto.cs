namespace Trivo.Application.DTOs.Reports;

public sealed record MessageReportDto(
    Guid MessageId,
    Guid? SenderId,
    string? Content,
    string? Type,
    DateTime? SentAt,
    UserReportDto? Sender
);
