using FluentValidation;

namespace Trivo.Application.Features.Users.Commands.ConfirmAccount;

public sealed class ConfirmAccountValidator : AbstractValidator<ConfirmAccountCommand>
{
    public ConfirmAccountValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("The user ID cannot be empty.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("The confirmation code is required.")
            .Matches(@"^\d{6}$").WithMessage("The confirmation code must contain exactly 6 numeric digits.");
    }
}
