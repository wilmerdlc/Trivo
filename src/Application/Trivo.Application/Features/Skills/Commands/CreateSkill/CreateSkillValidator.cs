using FluentValidation;

namespace Trivo.Application.Features.Skills.Commands.CreateSkill;

public class CreateSkillValidator : AbstractValidator<CreateSkillCommand>
{
    public CreateSkillValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The skill name is required.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("The user ID is required.");
    }
}