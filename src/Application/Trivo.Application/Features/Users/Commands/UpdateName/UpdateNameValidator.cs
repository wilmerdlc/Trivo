using FluentValidation;

namespace Trivo.Application.Features.Users.Commands.UpdateName;

public sealed class UpdateNameValidator : AbstractValidator<UpdateNameCommand>
{
    public UpdateNameValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("The user ID is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(30).WithMessage("First name must not exceed 30 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(30).WithMessage("Last name must not exceed 30 characters.");
    }
}
