using FluentValidation;

namespace Trivo.Application.Features.Users.Commands.UpdatePassword;

public sealed class UpdatePasswordValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("The user ID is required.");

        // No length checks on the old password: it's being verified against an existing hash.
        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("The old password is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("The new password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(30).WithMessage("Password must not exceed 30 characters.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("You must confirm the password.")
            .Equal(x => x.NewPassword).WithMessage("The password confirmation does not match.");
    }
}
