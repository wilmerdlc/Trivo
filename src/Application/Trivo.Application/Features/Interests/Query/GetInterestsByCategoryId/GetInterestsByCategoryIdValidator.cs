using FluentValidation;
using Trivo.Application.Pagination;

namespace Trivo.Application.Features.Interests.Query.GetInterestsByCategoryId;

public sealed class GetInterestsByCategoryIdValidator : AbstractValidator<GetInterestsByCategoryIdQuery>
{
    public GetInterestsByCategoryIdValidator()
    {
        RuleFor(x => x.CategoryIds)
            .NotEmpty().WithMessage("At least one category ID is required.");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, PaginationValidator.MaxPageSize)
            .WithMessage($"Page size must be between 1 and {PaginationValidator.MaxPageSize}.");
    }
}
