using FluentValidation;
using Trivo.Application.Pagination;

namespace Trivo.Application.Features.Users.Query.GetUsersByInterestsAndSkills;

public sealed class GetUsersByInterestsAndSkillsValidator : AbstractValidator<GetUsersByInterestsAndSkillsQuery>
{
    public GetUsersByInterestsAndSkillsValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, PaginationValidator.MaxPageSize)
            .WithMessage($"Page size must be between 1 and {PaginationValidator.MaxPageSize}.");
    }
}
