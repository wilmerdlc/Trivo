namespace Trivo.Domain.Models;

public sealed class Message
{
    public Guid? MessageId { get; set; }

    public Guid? ChatId { get; set; }

    public Guid? SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public string? Content { get; set; }

    public string? Type { get; set; }

    public string? Status { get; set; }

    public DateTime? SentAt { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public User? Sender { get; set; }

    public User? Receiver { get; set; }

    public Chat? Chat { get; set; }

    public ICollection<Report>? Reports { get; set; }
}