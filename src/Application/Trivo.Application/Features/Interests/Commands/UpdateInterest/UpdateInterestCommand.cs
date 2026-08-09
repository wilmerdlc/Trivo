using Trivo.Application.Abstractions.Messages;

namespace Trivo.Application.Features.Interests.Commands.UpdateInterest;

public sealed record UpdateInterestCommand(
    Guid UserId,
    IReadOnlyList<Guid> InterestIds
) : ICommand<string>;