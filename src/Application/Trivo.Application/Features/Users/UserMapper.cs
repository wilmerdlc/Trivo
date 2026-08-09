using Trivo.Domain.Models;

using Trivo.Application.DTOs.Interests;
using Trivo.Application.DTOs.Skills;
using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users;

public static class UserMapper
{
    public static UserDto MapToUserDto(Domain.Models.User user)
    {
        return new UserDto(
            Id: user.Id,
            FirstName: user.FirstName,
            LastName: user.LastName,
            ProfilePictureUrl: user.ProfilePicture
        );
    }

    public static UserDto MapUserDto(Domain.Models.User user) => MapToUserDto(user);

    public static UserAiRecommendationDto MapToAiRecommendationDto(Domain.Models.User user)
    {
        return new UserAiRecommendationDto(
            UserId: user.Id,
            FirstName: user.FirstName,
            LastName: user.LastName,
            Location: user.Location,
            Biography: user.Biography,
            Position: user.Position,
            ProfilePicture: user.ProfilePicture,
            Interests: MapToInterests(user.UserInterests ?? new List<UserInterest>()),
            Skills: MapToSkills(user.UserSkills ?? new List<UserSkill>())
        );
    }

    public static UserDetailsDto MapToUserDetailsDto(Domain.Models.User user)
    {
        return new UserDetailsDto(
            FirstName: user.FirstName,
            LastName: user.LastName,
            Location: user.Location,
            Biography: user.Biography,
            ProfilePicture: user.ProfilePicture,
            Position: user.Position,
            Skills: MapToSkills(user.UserSkills ?? new List<UserSkill>()),
            Interests: MapToInterests(user.UserInterests ?? new List<UserInterest>())
        );
    }

    public static ExpertDetailsDto MapToExpertDetailsDto(UserDetailsDto userDetails, Expert? expert)
    {
        return new ExpertDetailsDto(
            FirstName: userDetails.FirstName,
            LastName: userDetails.LastName,
            Location: userDetails.Location,
            Biography: userDetails.Biography,
            ProfilePicture: userDetails.ProfilePicture,
            Position: userDetails.Position,
            Skills: userDetails.Skills,
            Interests: userDetails.Interests,
            IsAvailableForProjects: expert?.AvailableForProjects,
            IsHired: expert?.IsHired
        );
    }

    public static RecruiterDetailsDto MapToRecruiterDetailsDto(UserDetailsDto userDetails, Recruiter? recruiter)
    {
        return new RecruiterDetailsDto(
            FirstName: userDetails.FirstName,
            LastName: userDetails.LastName,
            Location: userDetails.Location,
            Biography: userDetails.Biography,
            ProfilePicture: userDetails.ProfilePicture,
            Position: userDetails.Position,
            Skills: userDetails.Skills,
            Interests: userDetails.Interests,
            CompanyName: recruiter?.CompanyName
        );
    }

    public static List<InterestWithIdDto> MapToInterests(ICollection<UserInterest> userInterests)
    {
        return userInterests
            .Where(ui => ui.Interest is not null)
            .Select(ui => new InterestWithIdDto(
                InterestId: ui.Interest!.Id,
                Name: ui.Interest.Name ?? string.Empty))
            .ToList() ?? [];
    }

    public static List<SkillWithIdDto> MapToSkills(ICollection<UserSkill> userSkills)
    {
        return userSkills?
            .Where(us => us.Skill is not null)
            .Select(us => new SkillWithIdDto(
                SkillId: us.Skill!.SkillId ?? Guid.Empty,
                Name: us.Skill.Name ?? string.Empty))
            .ToList() ?? [];
    }
}