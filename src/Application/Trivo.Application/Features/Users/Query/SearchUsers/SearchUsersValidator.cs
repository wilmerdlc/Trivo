using FluentValidation;
using Trivo.Application.Pagination;

namespace Trivo.Application.Features.Users.Query.SearchUsers;

public sealed class SearchUsersValidator : AbstractValidator<SearchUsersQuery>
{
    public SearchUsersValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("The search text is required.")
            .MaximumLength(100).WithMessage("The search text must not exceed 100 characters.");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, PaginationValidator.MaxPageSize)
            .WithMessage($"Page size must be between 1 and {PaginationValidator.MaxPageSize}.");
    }
}
