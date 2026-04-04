using FluentValidation;

namespace Trivo.Application.Features.Interests.Commands.CreateInterest;

internal sealed class CreateInterestValidator : AbstractValidator<CreateInterestCommand>
{
    public CreateInterestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.");

        RuleFor(x => x.CategoryId)
            .NotNull()
            .WithMessage("A valid category must be specified.");

        RuleFor(x => x.CreatedBy)
            .NotNull()
            .WithMessage("The user creating the interest must be specified.");
    }
}