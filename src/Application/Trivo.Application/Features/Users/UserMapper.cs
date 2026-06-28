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