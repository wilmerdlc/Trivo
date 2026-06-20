using FluentValidation;

namespace Trivo.Application.Features.Matching.Commands.CreateMatch;

public class CreateMatchValidator : AbstractValidator<CreateMatchingCommand>
{
    public CreateMatchValidator()
    {
        RuleFor(x => x.ExpertId)
            .NotEmpty()
            .WithMessage("Expert ID is required and must be a valid GUID.");

        RuleFor(x => x.RecruiterId)
            .NotEmpty()
            .WithMessage("Recruiter ID is required and must be a valid GUID.");

        RuleFor(x => x.CreatedBy)
            .NotNull()
            .WithMessage("The creator of the match must be specified.");
    }
}