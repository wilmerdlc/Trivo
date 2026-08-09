using FluentValidation;
using Trivo.Domain.Enums;

namespace Trivo.Application.Features.Matching.Query.GetMatchByUser;

public class GetMatchByUserValidator : AbstractValidator<GetMatchByUserQuery>
{
    public GetMatchByUserValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("The user ID is required.")
            .Must(id => id != Guid.Empty).WithMessage("The user ID must be a valid GUID.");

        RuleFor(x => x.Role)
            .Must(role => role == Roles.Recruiter || role == Roles.Expert)
            .WithMessage("The role must be Recruiter or Expert.");
    }
}