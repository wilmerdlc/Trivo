using Trivo.Application.Features.Users;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Matching;

namespace Trivo.Application.Features.Matching;

public static class MatchMapper
{
    public static ExpertAiRecommendationDto MapToExpertDto(User user, Expert expert)
    {
        return new ExpertAiRecommendationDto(
            ExpertId: expert?.Id ?? Guid.Empty,
            UserId: user.Id,
            FirstName: user.FirstName ?? string.Empty,
            LastName: user.LastName ?? string.Empty,
            Location: user.Location ?? string.Empty,
            Biography: user.Biography ?? string.Empty,
            ProfilePicture: user.ProfilePicture ?? string.Empty,
            Position: user.Position ?? string.Empty,
            Interests: UserMapper.MapToInterests(user.UserInterests),
            Skills: UserMapper.MapToSkills(user.UserSkills),
            IsAvailableForProjects: expert?.AvailableForProjects ?? false,
            IsHired: expert?.IsHired ?? false
        );
    }

    public static RecruiterAiRecommendationDto MapToRecruiterDto(User user, Recruiter recruiter)
    {
        return new RecruiterAiRecommendationDto(
            RecruiterId: recruiter?.Id ?? Guid.Empty,
            UserId: user.Id,
            FirstName: user.FirstName ?? string.Empty,
            LastName: user.LastName ?? string.Empty,
            Location: user.Location ?? string.Empty,
            Biography: user.Biography ?? string.Empty,
            ProfilePicture: user.ProfilePicture ?? string.Empty,
            Position: user.Position ?? string.Empty,
            Interests: UserMapper.MapToInterests(user.UserInterests),
            Skills: UserMapper.MapToSkills(user.UserSkills),
            CompanyName: recruiter?.CompanyName
        );
    }

    public static MatchDto MapToMatchDtoForExpert(
        Match match,
        ExpertAiRecommendationDto expertDto,
        RecruiterAiRecommendationDto recruiterDto)
    {
        return new MatchDto(
            MatchId: match.Id,
            RecruiterId: null,
            ExpertId: match.ExpertId,
            MatchStatus: match.MatchStatus ?? string.Empty,
            ExpertStatus: match.ExpertStatus ?? string.Empty,
            RecruiterStatus: match.RecruiterStatus ?? string.Empty,
            CreatedAt: match.CreatedAt,
            ExpertDto: expertDto,
            RecruiterDto: recruiterDto
        );
    }

    public static MatchDto MapToMatchDtoForRecruiter(
        Match match,
        ExpertAiRecommendationDto expertDto,
        RecruiterAiRecommendationDto recruiterDto)
    {
        return new MatchDto(
            MatchId: match.Id,
            RecruiterId: match.RecruiterId,
            ExpertId: null,
            MatchStatus: match.MatchStatus ?? string.Empty,
            ExpertStatus: match.ExpertStatus ?? string.Empty,
            RecruiterStatus: match.RecruiterStatus ?? string.Empty,
            CreatedAt: match.CreatedAt,
            ExpertDto: expertDto,
            RecruiterDto: recruiterDto
        );
    }

    public static MatchDetailsDto MapToMatchDetailsDto(Match match)
    {
        return new MatchDetailsDto(
            MatchId: match.Id,
            RecruiterId: match.RecruiterId ?? Guid.Empty,
            ExpertId: match.ExpertId ?? Guid.Empty,
            ExpertStatus: match.ExpertStatus ?? string.Empty,
            RecruiterStatus: match.RecruiterStatus ?? string.Empty,
            MatchStatus: match.MatchStatus ?? string.Empty,
            CreatedAt: match.CreatedAt
        );
    }
}