using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.DTOs.Administrator;

public sealed record AdminDto(
    Guid? AdminId,
    string? FirstName,
    string? LastName,
    string? Biography,
    string? Email,
    string? Username,
    string? ProfilePhotoUrl,
    DateTime? RegisteredAt
);