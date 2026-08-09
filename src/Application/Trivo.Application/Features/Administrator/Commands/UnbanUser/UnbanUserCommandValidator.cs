using FluentValidation;

namespace Trivo.Application.Features.Administrator.Commands.UnbanUser;

public sealed class UnbanUserCommandValidator : AbstractValidator<UnbanUserCommand>
{
    public UnbanUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");
    }
}