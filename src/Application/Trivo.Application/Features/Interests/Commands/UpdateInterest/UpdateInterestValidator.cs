using FluentValidation;

namespace Trivo.Application.Features.Interests.Commands.UpdateInterest;

internal sealed class UpdateInterestValidator : AbstractValidator<UpdateInterestCommand>
{
    public UpdateInterestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");

        RuleFor(x => x.InterestIds)
            .NotEmpty()
            .WithMessage("At least one interest must be provided.")
            .Must(ids => ids.Distinct().Count() == ids.Count)
            .WithMessage("Duplicate interests are not allowed.");
    }
}