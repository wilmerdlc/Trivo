using Trivo.Application.DTOs.Users;

namespace Trivo.Application.DTOs.Users;

public sealed record UserDto(
    Guid Id,
    string? FirstName,
    string? LastName,
    string? ProfilePictureUrl
);