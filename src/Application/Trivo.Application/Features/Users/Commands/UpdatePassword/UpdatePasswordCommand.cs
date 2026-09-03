using Trivo.Application.Abstractions.Messages;

namespace Trivo.Application.Features.Users.Commands.UpdatePassword;

public sealed record UpdatePasswordCommand(
    Guid UserId,
    string OldPassword,
    string NewPassword,
    string ConfirmPassword
) : ICommand<string>, IUserOwnedRequest;
