using FluentValidation;

namespace Trivo.Application.Features.Users.Commands.ResendConfirmationCode;

public sealed class ResendConfirmationCodeValidator : AbstractValidator<ResendConfirmationCodeCommand>
{
    public ResendConfirmationCodeValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address must be provided.");
    }
}
