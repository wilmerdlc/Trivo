using FluentValidation;

namespace Trivo.Application.Features.Skills.Query.SearchSkillsByName;

public class SearchSkillsByNameValidator : AbstractValidator<SearchSkillsByNameQuery>
{
    public SearchSkillsByNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The skill name is required to perform a search.");
    }
}