using FluentValidation;

namespace Trivo.Application.Features.Users.Commands.ResetPassword;

public sealed class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address must be provided.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("The verification code is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("The new password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(30).WithMessage("Password must not exceed 30 characters.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("You must confirm the password.")
            .Equal(x => x.NewPassword).WithMessage("The password confirmation does not match the new password.");
    }
}
