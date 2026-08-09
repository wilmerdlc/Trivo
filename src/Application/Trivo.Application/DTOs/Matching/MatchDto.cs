
using Trivo.Application.DTOs.Matching;

namespace Trivo.Application.DTOs.Matching;

public record MatchDto(
    Guid MatchId,
    Guid? RecruiterId,
    Guid? ExpertId,
    string? ExpertStatus,
    string? RecruiterStatus,
    string? MatchStatus,
    DateTime? CreatedAt,
    ExpertAiRecommendationDto? ExpertDto,
    RecruiterAiRecommendationDto? RecruiterDto
    );