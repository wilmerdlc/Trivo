using Trivo.Application.DTOs.Email;

namespace Trivo.Application.DTOs.Email;

public sealed record EmailResponseDto(
    string User,
    string? Body,
    string? Subject
);