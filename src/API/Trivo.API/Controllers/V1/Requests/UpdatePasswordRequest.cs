namespace Trivo.API.Controllers.V1.Requests;

public sealed record UpdatePasswordRequest(string OldPassword, string NewPassword, string ConfirmPassword);
