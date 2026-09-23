namespace Trivo.Domain.Enums;

public enum SanctionType
{
    /// <summary>Recorded and notified, but doesn't restrict the account.</summary>
    Warning,
    TemporarySuspension,
    PermanentBan
}
