using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Expert;

namespace Trivo.Application.Features.Experts.Commands.UpdateExpert;

public sealed record UpdateExpertCommand(
    Guid ExpertId,
    Guid RequesterId,
    bool AvailableForProjects,
    bool Hired
) : ICommand<ExpertDto>;
