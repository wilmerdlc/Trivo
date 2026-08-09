
using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.DTOs.Administrator;

public sealed record AdminMatchDto(
    Guid MatchId,
    Guid RecruiterId,
    Guid ExpertId,
    string? ExpertStatus,
    string? RecruiterStatus,
    string? MatchStatus,
    DateTime? CreatedAt,
    RecruiterMatchDto? Recruiter,
    ExpertMatchDto? Expert
);