using FluentValidation;
using Trivo.Domain.Enums;

namespace Trivo.Application.Features.Reports.Commands.ResolveReport;

public sealed class ResolveReportValidator : AbstractValidator<ResolveReportCommand>
{
    public const int MaxSuspensionDays = 365;

    public ResolveReportValidator()
    {
        RuleFor(x => x.ReportId)
            .NotEmpty().WithMessage("The report ID is required.");

        RuleFor(x => x.AdminId)
            .NotEmpty().WithMessage("The administrator ID is required.");

        RuleFor(x => x.Decision)
            .NotEmpty().WithMessage("The decision is required.")
            .Must(d => Enum.TryParse<ReportDecision>(d, ignoreCase: true, out _))
            .WithMessage("The decision must be 'Approved' or 'Rejected'.");

        RuleFor(x => x.FinalReason)
            .NotEmpty().WithMessage("A final reason explaining the decision is required.")
            .MaximumLength(1000).WithMessage("The final reason must not exceed 1000 characters.");

        When(x => IsDecision(x, ReportDecision.Approved), () =>
        {
            RuleFor(x => x.SanctionType)
                .NotEmpty().WithMessage("A sanction type is required when approving a report.")
                .Must(t => Enum.TryParse<SanctionType>(t, ignoreCase: true, out _))
                .WithMessage("The sanction type must be 'Warning', 'TemporarySuspension' or 'PermanentBan'.");

            When(x => IsSanction(x, SanctionType.TemporarySuspension), () =>
            {
                RuleFor(x => x.DurationDays)
                    .NotNull().WithMessage("The duration in days is required for a temporary suspension.")
                    .InclusiveBetween(1, MaxSuspensionDays)
                    .WithMessage($"The suspension must last between 1 and {MaxSuspensionDays} days.");
            });

            When(x => !IsSanction(x, SanctionType.TemporarySuspension), () =>
            {
                RuleFor(x => x.DurationDays)
                    .Null().WithMessage("A duration only applies to a temporary suspension.");
            });
        });

        When(x => IsDecision(x, ReportDecision.Rejected), () =>
        {
            RuleFor(x => x.SanctionType)
                .Null().WithMessage("A rejected report can't carry a sanction.");

            RuleFor(x => x.DurationDays)
                .Null().WithMessage("A rejected report can't carry a sanction.");
        });
    }

    private static bool IsDecision(ResolveReportCommand command, ReportDecision decision) =>
        Enum.TryParse<ReportDecision>(command.Decision, ignoreCase: true, out var parsed) && parsed == decision;

    private static bool IsSanction(ResolveReportCommand command, SanctionType type) =>
        Enum.TryParse<SanctionType>(command.SanctionType, ignoreCase: true, out var parsed) && parsed == type;
}
