using FluentValidation;

namespace Trivo.Application.Features.Recruiters.Commands.UpdateRecruiter;

public sealed class UpdateRecruiterValidator : AbstractValidator<UpdateRecruiterCommand>
{
    public UpdateRecruiterValidator()
    {
        RuleFor(x => x.RecruiterId)
            .NotEmpty().WithMessage("Recruiter ID is required.");

        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage("Company name is required.")
            .MaximumLength(100).WithMessage("Company name must not exceed 100 characters.");
    }
}
