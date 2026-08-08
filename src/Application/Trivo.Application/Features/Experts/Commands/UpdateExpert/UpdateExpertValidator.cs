using FluentValidation;

namespace Trivo.Application.Features.Experts.Commands.UpdateExpert;

public sealed class UpdateExpertValidator : AbstractValidator<UpdateExpertCommand>
{
    public UpdateExpertValidator()
    {
        RuleFor(x => x.ExpertId)
            .NotEmpty().WithMessage("Expert ID is required.");
    }
}
