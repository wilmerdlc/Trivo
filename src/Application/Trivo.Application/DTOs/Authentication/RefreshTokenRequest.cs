using Trivo.Application.DTOs.Authentication;

namespace Trivo.Application.DTOs.Authentication;

public class RefreshTokenRequest
{
    public string? RefreshToken { get; set; }
}