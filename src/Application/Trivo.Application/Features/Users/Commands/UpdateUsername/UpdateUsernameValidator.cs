using FluentValidation;

namespace Trivo.Application.Features.Users.Commands.UpdateUsername;

public sealed class UpdateUsernameValidator : AbstractValidator<UpdateUsernameCommand>
{
    public UpdateUsernameValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("The user ID is required.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");
    }
}
