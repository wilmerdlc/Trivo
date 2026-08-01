using FluentValidation;

namespace Trivo.Application.Features.Users.Commands.ChangePassword;

public sealed class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address must be provided.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("The new password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .MaximumLength(30).WithMessage("Password must not exceed 30 characters.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("You must confirm the password.")
            .Equal(x => x.NewPassword).WithMessage("The password confirmation does not match.");
    }
}
