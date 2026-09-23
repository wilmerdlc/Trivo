namespace Trivo.Domain.Enums;

public enum UserStatus
{
    Banned,
    Active,
    Inactive,

    /// <summary>Temporarily blocked by a sanction; lifted automatically once it expires.</summary>
    Suspended
}
