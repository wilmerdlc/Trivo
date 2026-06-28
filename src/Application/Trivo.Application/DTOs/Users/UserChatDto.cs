using Trivo.Application.DTOs.Users;

namespace Trivo.Application.DTOs.Users;

public record UserChatDto(
    Guid? UserId,
    string Username,
    string FullName,
    string? ProfilePicture
);