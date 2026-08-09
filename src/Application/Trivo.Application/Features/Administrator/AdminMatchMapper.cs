using Trivo.Domain.Models;

using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.Features.Administrator;

public static class AdminMatchMapper
{
    public static AdminMatchDto ToAdminDto(this Match match)
        => new(
            MatchId: match.Id,
            RecruiterId: match.RecruiterId ?? Guid.Empty,
            ExpertId: match.ExpertId ?? Guid.Empty,
            ExpertStatus: match.ExpertStatus,
            RecruiterStatus: match.RecruiterStatus,
            MatchStatus: match.MatchStatus,
            CreatedAt: match.CreatedAt,
            Recruiter: new RecruiterMatchDto(
                FirstName: match.Recruiter!.User!.FirstName,
                LastName: match.Recruiter.User.LastName!
            ),
            Expert: new ExpertMatchDto(
                FirstName: match.Expert!.User!.FirstName!,
                LastName: match.Expert.User.LastName!
            )
        );
}