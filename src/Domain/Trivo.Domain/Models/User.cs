using Pgvector;
using Trivo.Domain.Common;

namespace Trivo.Domain.Models;

public sealed class User : BaseEntity
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Biography { get; set; }

    public bool? IsAccountConfirmed { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public string? Username { get; set; }

    public string? Location { get; set; }

    public string? ProfilePicture { get; set; }

    public string? LinkedIn { get; set; }

    public string? UserStatus { get; set; }

    public string? Position { get; set; }
    
    public Vector? ProfileEmbedding { get; set; }

    /// <summary>
    /// SHA-256 hash of the text last sent to the embedding provider for this profile. Lets the
    /// regeneration flow skip the call entirely when the profile text hasn't actually changed —
    /// embedding calls aren't perfectly deterministic across invocations, so re-embedding
    /// unchanged text can shuffle a user's ranking for no real reason.
    /// </summary>
    public string? ProfileTextHash { get; set; }

    // Relationships
    public ICollection<Code>? Codes { get; set; }

    public ICollection<UserInterest>? UserInterests { get; set; }

    public ICollection<UserSkill>? UserSkills { get; set; }

    public ICollection<ChatUser>? ChatUsers { get; set; }

    public ICollection<Message>? SentMessages { get; set; }

    public ICollection<Message>? ReceivedMessages { get; set; }

    public ICollection<Notification>? Notifications { get; set; }

    public ICollection<Expert>? Experts { get; set; }

    public ICollection<Recruiter>? Recruiters { get; set; }

    public ICollection<Report>? Reports { get; set; }
}