using Trivo.Application.Abstractions.Messages;

namespace Trivo.Application.Features.Administrator.Commands.BanUser;

public sealed record BanUserCommand(Guid UserId) : ICommand<string>;