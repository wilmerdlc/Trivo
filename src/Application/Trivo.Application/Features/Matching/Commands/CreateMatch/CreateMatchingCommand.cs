using Trivo.Application.Abstractions.Messages;
using Trivo.Application.DTOs.Match;
using Trivo.Domain.Enums;

namespace Trivo.Application.Features.Matching.Commands.CreateMatch;

public sealed record CreateMatchingCommand(
    Guid? RecruiterId,
    Guid? ExpertId,
    Roles? CreatedBy
) : ICommand<MatchDetailsDto>;