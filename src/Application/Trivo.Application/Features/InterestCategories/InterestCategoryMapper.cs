using Trivo.Application.Features.InterestCategories.Commands.CreateInterestCategory;
using Trivo.Domain.Models;
using Trivo.Application.DTOs.InterestCategories;

namespace Trivo.Application.Features.InterestCategories;

public static class InterestCategoryMapper
{
    public static InterestCategory ToEntity(this CreateInterestCategoryCommand command)
    {
        return new InterestCategory
        {
            CategoryId = Guid.NewGuid(),
            Name = command.Name
        };
    }

    public static InterestCategoryDto ToDto(this InterestCategory entity)
    {
        return new InterestCategoryDto
        (
            InterestCategoryId: entity.CategoryId,
            Name: entity.Name!
        );
    }

    public static List<InterestCategoryDto> ToDtoList(this IEnumerable<InterestCategory>? entities)
    {
        IEnumerable<InterestCategory> interestCategories = entities as InterestCategory[] ?? entities!.ToArray();

        return !interestCategories.Any() ? [] : interestCategories.Select(entity => entity.ToDto()).ToList();
    }
}