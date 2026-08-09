using FluentValidation;

namespace Trivo.Application.Features.Experts.Commands.CreateExpert;

public sealed class CreateExpertValidator : AbstractValidator<CreateExpertCommand>
{
    public CreateExpertValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}
