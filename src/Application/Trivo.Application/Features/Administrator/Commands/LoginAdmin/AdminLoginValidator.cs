using FluentValidation;

namespace Trivo.Application.Features.Administrator.Commands.LoginAdmin;

public sealed class AdminLoginCommandValidator : AbstractValidator<AdminLoginCommand>
{
    public AdminLoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address must be provided.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

        // No length checks: this validates a password against an existing hash, not a new one.
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}