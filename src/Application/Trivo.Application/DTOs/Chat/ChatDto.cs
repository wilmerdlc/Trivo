
using Trivo.Application.DTOs.Chat;
using Trivo.Application.DTOs.Users;

namespace Trivo.Application.DTOs.Chat;

public sealed record ChatDto(
    Guid Id,
    List<UserChatDto> Participants,
    DateTime CreatedAt,
    string Name,
    MessageDto? LastMessage
);