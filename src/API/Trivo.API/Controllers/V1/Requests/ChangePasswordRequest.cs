namespace Trivo.API.Controllers.V1.Requests;

public sealed record ChangePasswordRequest(string Code, string NewPassword, string ConfirmPassword);
