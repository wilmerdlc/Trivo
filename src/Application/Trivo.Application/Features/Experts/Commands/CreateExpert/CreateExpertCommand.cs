using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Expert;

namespace Trivo.Application.Features.Experts.Commands.CreateExpert;

public sealed record CreateExpertCommand(
    Guid UserId,
    bool AvailableForProjects,
    bool Hired
) : ICommand<ExpertDto>;
