using Trivo.Application.DTOs.Interests;
using Trivo.Application.Features.Interests.Commands.CreateInterest;
using Trivo.Domain.Models;

namespace Trivo.Application.Features.Interests;

public static class InterestMapper
{
    public static InterestDetailsDto ToInterestDetailsDto(this Interest interest) =>
        new(
            InterestId: interest.Id,
            Name: interest.Name ?? string.Empty,
            CategoryId: interest.CategoryId,
            CreatedBy: interest.CreatedBy ?? Guid.Empty
        );

    public static IEnumerable<InterestWithIdDto> ToInterestWithIdDtoList(this IEnumerable<Interest> interests) =>
        interests.Select(ToInterestWithIdDto).ToList();

    public static IEnumerable<InterestByCategoryIdDto> ToInterestByCategoryIdDtoList(
        this IEnumerable<Interest> interests) =>
        interests.Select(ToInterestByCategoryIdDto).ToList();

    public static IEnumerable<InterestDto> ToInterestDtoList(this IEnumerable<Interest> interests) =>
        interests.Select(ToInterestDto).ToList();

    public static Interest ToInterestEntity(this CreateInterestCommand command, Guid id)
    {
        return new Interest
        {
            Id = id,
            CategoryId = command.CategoryId,
            Name = command.Name,
            CreatedBy = command.CreatedBy,
        };
    }

    public static UserInterest ToUserInterestEntity(this CreateInterestCommand command, Guid interestId)
    {
        return new UserInterest
        {
            UserId = command.CreatedBy,
            InterestId = interestId
        };
    }

    #region Private Methods

    private static InterestWithIdDto ToInterestWithIdDto(this Interest interest) =>
        new(
            InterestId: interest.Id,
            Name: interest.Name ?? string.Empty
        );

    private static InterestByCategoryIdDto ToInterestByCategoryIdDto(this Interest interest) =>
        new(
            InterestId: interest.Id,
            Name: interest.Name ?? string.Empty
        );

    private static InterestDto ToInterestDto(this Interest interest) =>
        new(
            InterestId: interest.Id,
            Name: interest.Name ?? string.Empty
        );

    #endregion
}