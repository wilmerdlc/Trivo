using FluentValidation;

namespace Trivo.Application.Features.Matching.Commands.CreateMatchRejection;

public class CreateMatchRejectionValidator : AbstractValidator<CreateMatchRejectionCommand>
{
    public CreateMatchRejectionValidator()
    {
        RuleFor(x => x.ExpertId)
            .NotEmpty()
            .WithMessage("Expert ID is required and must be a valid GUID.");

        RuleFor(x => x.RecruiterId)
            .NotEmpty()
            .WithMessage("Recruiter ID is required and must be a valid GUID.");

        RuleFor(x => x.CreatedBy)
            .NotNull()
            .WithMessage("The creator of the rejection must be specified.");
    }
}