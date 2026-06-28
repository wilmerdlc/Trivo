
using Trivo.Application.DTOs.Interests;
using Trivo.Application.DTOs.Skills;
using Trivo.Application.DTOs.Users;

namespace Trivo.Application.DTOs.Users;

public record UserAiRecommendationDto(
    Guid UserId,
    string? FirstName,
    string? LastName,
    string? Location,
    string? Biography,
    string? Position,
    string? ProfilePicture,
    List<InterestWithIdDto> Interests,
    List<SkillWithIdDto> Skills
    );