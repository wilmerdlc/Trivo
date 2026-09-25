namespace Trivo.Application.DTOs.Users;

public sealed record UserDto(
    Guid UserId,
    string? FirstName,
    string? LastName,
    string? Email,
    string? ProfilePicture,
    DateTime? CreatedAt = null
);
