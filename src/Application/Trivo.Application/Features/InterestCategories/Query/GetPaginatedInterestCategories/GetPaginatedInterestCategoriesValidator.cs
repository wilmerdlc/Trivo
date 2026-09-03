using FluentValidation;
using Trivo.Application.Pagination;

namespace Trivo.Application.Features.InterestCategories.Query.GetPaginatedInterestCategories;

public sealed class GetPaginatedInterestCategoriesValidator : AbstractValidator<GetPaginatedInterestCategoriesQuery>
{
    public GetPaginatedInterestCategoriesValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, PaginationValidator.MaxPageSize)
            .WithMessage($"Page size must be between 1 and {PaginationValidator.MaxPageSize}.");
    }
}
