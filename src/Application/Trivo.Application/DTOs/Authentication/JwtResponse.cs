using Trivo.Application.DTOs.Authentication;

namespace Trivo.Application.DTOs.Authentication;

public sealed record JwtResponse(bool HasError, string? Error);