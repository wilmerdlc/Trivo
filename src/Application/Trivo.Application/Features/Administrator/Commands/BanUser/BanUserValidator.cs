namespace Trivo.Application.Features.Administrator.Commands.BanUser;

using FluentValidation;

public class BanUserValidator : AbstractValidator<BanUserCommand>
{
    public BanUserValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");
    }
}