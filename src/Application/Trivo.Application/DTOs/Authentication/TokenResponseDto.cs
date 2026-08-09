using Trivo.Application.DTOs.Authentication;

namespace Trivo.Application.DTOs.Authentication;

public class TokenResponseDto
{
    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }
}