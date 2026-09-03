using FluentValidation;

namespace Trivo.Application.Features.Users.Commands.LoginUser;

public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address must be provided.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

        // No MinimumLength/MaximumLength here on purpose: this checks a password against an
        // existing hash, not creating one. Enforcing today's policy at login would lock out any
        // account created under an older (or future, different) policy.
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}
