using Trivo.Application.DTOs.Interests;
using Trivo.Application.DTOs.Skills;

namespace Trivo.Application.DTOs.Users;

public sealed record RecruiterDetailsDto(
    string? FirstName,
    string? LastName,
    string? Location,
    string? Biography,
    string? ProfilePicture,
    string? Position,
    List<SkillWithIdDto> Skills,
    List<InterestWithIdDto> Interests,
    string? CompanyName
) : UserDetailsDto(FirstName, LastName, Location, Biography, ProfilePicture, Position, Skills, Interests);
