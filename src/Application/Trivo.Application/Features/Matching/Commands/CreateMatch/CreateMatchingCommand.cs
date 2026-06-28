using Trivo.Application.Abstractions.Messages;
using Trivo.Domain.Enums;

using Trivo.Application.DTOs.Matching;

namespace Trivo.Application.Features.Matching.Commands.CreateMatch;

public sealed record CreateMatchingCommand(
    Guid? RecruiterId,
    Guid? ExpertId,
    Roles? CreatedBy
) : ICommand<MatchDetailsDto>;