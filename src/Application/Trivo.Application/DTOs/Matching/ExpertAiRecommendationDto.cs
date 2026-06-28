
using Trivo.Application.DTOs.Interests;
using Trivo.Application.DTOs.Matching;
using Trivo.Application.DTOs.Skills;
using Trivo.Application.DTOs.Users;

namespace Trivo.Application.DTOs.Matching;
public record ExpertAiRecommendationDto(
    Guid ExpertId,
    Guid UserId,
    string? FirstName,
    string? LastName,
    string? Location,
    string? Biography,
    string? ProfilePicture,
    string? Position,
    List<InterestWithIdDto> Interests,
    List<SkillWithIdDto> Skills,
    bool? IsAvailableForProjects,
    bool? IsHired
) : UserAiRecommendationDto(UserId, FirstName, LastName, Location, Biography, Position, ProfilePicture, Interests,
    Skills);