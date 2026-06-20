using FluentValidation;

namespace Trivo.Application.Features.Skills.Commands.UpdateSkill;

public class UpdateSkillValidator : AbstractValidator<UpdateSkillCommand>
{
    public UpdateSkillValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("The user ID is required.");

        RuleFor(x => x.SkillIds)
            .NotNull().WithMessage("The skill list must be provided.")
            .Must(ids => ids.Any()).WithMessage("At least one skill must be provided.")
            .Must(ids => ids.Distinct().Count() == ids.Count)
            .WithMessage("Duplicate skill IDs are not allowed.");
    }
}