using Trivo.Application.DTOs.Matching;

namespace Trivo.Application.DTOs.Matching;

public sealed record MatchDetailsDto(
    Guid MatchId,
    Guid RecruiterId,
    Guid ExpertId,
    string ExpertStatus,
    string RecruiterStatus,
    string MatchStatus,
    DateTime? CreatedAt
);