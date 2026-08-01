using Trivo.Application.DTOs.Interests;
using Trivo.Application.DTOs.Skills;

namespace Trivo.Application.DTOs.Users;

public sealed record ExpertDetailsDto(
    string? FirstName,
    string? LastName,
    string? Location,
    string? Biography,
    string? ProfilePicture,
    string? Position,
    List<SkillWithIdDto> Skills,
    List<InterestWithIdDto> Interests,
    bool? IsAvailableForProjects,
    bool? IsHired
) : UserDetailsDto(FirstName, LastName, Location, Biography, ProfilePicture, Position, Skills, Interests);
