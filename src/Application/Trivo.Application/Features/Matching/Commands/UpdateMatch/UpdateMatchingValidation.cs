using FluentValidation;

namespace Trivo.Application.Features.Matching.Commands.UpdateMatch;

public class UpdateMatchingValidation : AbstractValidator<UpdateMatchingCommand>
{
    public UpdateMatchingValidation()
    {
        RuleFor(x => x.MatchingId)
            .NotEmpty().WithMessage("The matching ID is required")
            .WithErrorCode("400");

        RuleFor(x => x.MissingByMatching)
            .NotNull().WithMessage("You must specify who is performing the update (Expert or Recruiter)")
            .IsInEnum().WithMessage("Invalid role for the update")
            .WithErrorCode("400");

        RuleFor(x => x.Status)
            .NotNull().WithMessage("The update status is required")
            .IsInEnum().WithMessage("Invalid matching status")
            .WithErrorCode("400");
    }
}