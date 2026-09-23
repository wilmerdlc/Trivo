using FluentValidation;
using Trivo.Application.Pagination;

namespace Trivo.Application.Features.Administrator.Query.GetExpertsPaged;

public sealed class GetExpertsPagedValidator : AbstractValidator<GetExpertsPagedQuery>
{
    public GetExpertsPagedValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, PaginationValidator.MaxPageSize)
            .WithMessage($"Page size must be between 1 and {PaginationValidator.MaxPageSize}.");
    }
}
