namespace Trivo.Application.DTOs.Users;

public sealed record UpdateUserDto(
    string? Username,
    string? Email
);
