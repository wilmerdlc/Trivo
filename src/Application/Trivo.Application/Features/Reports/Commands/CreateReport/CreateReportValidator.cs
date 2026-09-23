using FluentValidation;
using Trivo.Domain.Enums;

namespace Trivo.Application.Features.Reports.Commands.CreateReport;

public sealed class CreateReportValidator : AbstractValidator<CreateReportCommand>
{
    public CreateReportValidator()
    {
        RuleFor(x => x.ReportedById)
            .NotEmpty().WithMessage("The reporting user's ID is required.");

        RuleFor(x => x.Note)
            .NotEmpty().WithMessage("A note must be provided for the report.")
            .MaximumLength(250).WithMessage("The report note must not exceed 250 characters.");

        RuleFor(x => x.ReportType)
            .Must(type => type is null || Enum.TryParse<ReportType>(type, ignoreCase: true, out _))
            .WithMessage("The report type must be 'Message' or 'Profile'.");

        When(x => CreateReportCommandHandler.ResolveType(x) == ReportType.Message, () =>
        {
            RuleFor(x => x.MessageId)
                .NotNull().WithMessage("The message ID to report is required.")
                .NotEqual(Guid.Empty).WithMessage("The message ID to report is required.");
        });

        When(x => CreateReportCommandHandler.ResolveType(x) == ReportType.Profile, () =>
        {
            RuleFor(x => x.ReportedUserId)
                .NotNull().WithMessage("The ID of the user to report is required.")
                .NotEqual(Guid.Empty).WithMessage("The ID of the user to report is required.");

            RuleFor(x => x.MessageId)
                .Null().WithMessage("A profile report can't reference a message.");
        });
    }
}
