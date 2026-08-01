using FluentValidation;

namespace Trivo.Application.Features.Users.Commands.ResetPassword;

public sealed class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("The user ID is required.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("The verification code is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("The new password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .MaximumLength(30).WithMessage("Password must not exceed 30 characters.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("You must confirm the password.")
            .Equal(x => x.NewPassword).WithMessage("The password confirmation does not match the new password.");
    }
}
