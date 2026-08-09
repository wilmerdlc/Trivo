using Trivo.Application.Features.Experts.Commands.CreateExpert;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Expert;

namespace Trivo.Application.Features.Experts;

public static class ExpertMapper
{
    public static ExpertDto ToExpertDto(this Expert expert) =>
        new(
            Id: expert.Id,
            AvailableForProjects: expert.AvailableForProjects,
            IsHired: expert.IsHired,
            UserId: expert.UserId ?? Guid.Empty
        );

    public static Expert ToExpertEntity(this CreateExpertCommand command, Guid id) =>
        new()
        {
            Id = id,
            UserId = command.UserId,
            AvailableForProjects = command.AvailableForProjects,
            IsHired = command.Hired
        };
}
