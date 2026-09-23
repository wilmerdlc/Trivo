using FluentValidation;

namespace Trivo.Application.Features.Reports.Query.GetReportById;

public sealed class GetReportByIdValidator : AbstractValidator<GetReportByIdQuery>
{
    public GetReportByIdValidator()
    {
        RuleFor(x => x.ReportId)
            .NotEmpty().WithMessage("The report ID is required.");
    }
}
