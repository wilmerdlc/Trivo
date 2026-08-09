using FluentValidation;

namespace Trivo.Application.Features.Reports.Commands.CreateReport;

public sealed class CreateReportValidator : AbstractValidator<CreateReportCommand>
{
    public CreateReportValidator()
    {
        RuleFor(x => x.ReportedById)
            .NotEmpty().WithMessage("The reporting user's ID is required.");

        RuleFor(x => x.MessageId)
            .NotEmpty().WithMessage("The message ID to report is required.");

        RuleFor(x => x.Note)
            .NotEmpty().WithMessage("A note must be provided for the report.")
            .MaximumLength(250).WithMessage("The report note must not exceed 250 characters.");
    }
}
