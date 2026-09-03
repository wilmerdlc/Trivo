using Trivo.Domain.Common;

namespace Trivo.Domain.Models;

public sealed class Interest : BaseEntity
{
    public string? Name { get; set; }

    public Guid? CategoryId { get; set; }

    public InterestCategory? Category { get; set; }

    public Guid? CreatedBy { get; set; }

    public User? User { get; set; }

    public ICollection<UserInterest> UserInterests { get; set; } = new List<UserInterest>();
}