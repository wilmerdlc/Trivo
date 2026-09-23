using FluentValidation;
using Trivo.Application.Pagination;
using Trivo.Domain.Enums;

namespace Trivo.Application.Features.Reports.Query.GetReportsPaged;

public sealed class GetReportsPagedValidator : AbstractValidator<GetReportsPagedQuery>
{
    public GetReportsPagedValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, PaginationValidator.MaxPageSize)
            .WithMessage($"Page size must be between 1 and {PaginationValidator.MaxPageSize}.");

        RuleFor(x => x.Status)
            .Must(s => s is null || Enum.TryParse<ReportStatus>(s, ignoreCase: true, out _))
            .WithMessage("The status must be 'Pending', 'Approved' or 'Rejected'.");
    }
}
