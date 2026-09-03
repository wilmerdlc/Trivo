using FluentValidation;
using Trivo.Application.Pagination;
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

        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, PaginationValidator.MaxPageSize)
            .WithMessage($"Page size must be between 1 and {PaginationValidator.MaxPageSize}.");
    }
}