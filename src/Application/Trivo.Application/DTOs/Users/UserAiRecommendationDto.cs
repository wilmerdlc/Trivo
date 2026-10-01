using System.Text.Json.Serialization;
using Trivo.Application.DTOs.Interests;
using Trivo.Application.DTOs.Matching;
using Trivo.Application.DTOs.Skills;

namespace Trivo.Application.DTOs.Users;

[JsonDerivedType(typeof(RecruiterAiRecommendationDto))]
[JsonDerivedType(typeof(ExpertAiRecommendationDto))]
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