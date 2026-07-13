using FluentValidation;

namespace Trivo.Application.Features.Recruiters.Commands.CreateRecruiter;

public sealed class CreateRecruiterValidator : AbstractValidator<CreateRecruiterCommand>
{
    public CreateRecruiterValidator()
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage("Company name is required.")
            .MaximumLength(100).WithMessage("Company name must not exceed 100 characters.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}
