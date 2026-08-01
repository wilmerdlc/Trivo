using FluentValidation;

namespace Trivo.Application.Features.Users.Commands.UpdateBiography;

public sealed class UpdateBiographyValidator : AbstractValidator<UpdateBiographyCommand>
{
    public UpdateBiographyValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("The user ID is required.");

        RuleFor(x => x.Biography)
            .NotEmpty().WithMessage("Biography is required.");
    }
}
