using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Authentication;

namespace Trivo.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController(
    IAuthenticationService authenticationService
) : ControllerBase
{
    [HttpPost("refresh-token")]
    [SwaggerOperation(
        Summary = "Refresh token",
        Description = "Refreshes the JWT access token using a valid refresh token."
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ResultT<TokenResponseDto>> RefreshTokenAsync(
        [FromBody] RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        return await authenticationService.RefreshTokenAsync(request.RefreshToken!, cancellationToken);
    }
}
