using Microsoft.AspNetCore.Http;

namespace Trivo.API.Controllers.V1.Requests;

public sealed record UpdateProfilePictureRequest(IFormFile Image);
