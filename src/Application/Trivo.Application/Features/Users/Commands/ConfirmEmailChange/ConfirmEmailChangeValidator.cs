using FluentValidation;

namespace Trivo.Application.Features.Users.Commands.ConfirmEmailChange;

public sealed class ConfirmEmailChangeValidator : AbstractValidator<ConfirmEmailChangeCommand>
{
    public ConfirmEmailChangeValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("The user ID is required.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("The verification code is required.");
    }
}
