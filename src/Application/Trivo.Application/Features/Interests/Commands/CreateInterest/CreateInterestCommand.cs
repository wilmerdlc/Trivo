using Trivo.Application.Abstractions.Messages;
using Trivo.Application.DTOs.Interests;

namespace Trivo.Application.Features.Interests.Commands.CreateInterest;

public sealed record CreateInterestCommand(
    string Name,
    Guid? CategoryId,
    Guid? CreatedBy
) : ICommand<InterestDetailsDto>;