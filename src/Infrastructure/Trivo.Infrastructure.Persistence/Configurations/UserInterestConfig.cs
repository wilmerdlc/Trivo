using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trivo.Domain.Models;

namespace Trivo.Infrastructure.Persistence.Configurations;

public class UserInterestConfig : IEntityTypeConfiguration<UserInterest>
{
    public void Configure(EntityTypeBuilder<UserInterest> builder)
    {
        // Table Mapping
        builder.ToTable("UserInterest");

        // Composite Primary Key
        builder.HasKey(ui => new { ui.UserId, ui.InterestId });

        // Properties
        builder.Property(ui => ui.UserId)
            .HasColumnName("FKUserId")
            .IsRequired();

        builder.Property(ui => ui.InterestId)
            .HasColumnName("FKInterestId")
            .IsRequired();

        // Relationships
        builder.HasOne(ui => ui.User)
            .WithMany(u => u.UserInterests)
            .HasForeignKey(ui => ui.UserId);

        builder.HasOne(ui => ui.Interest)
            .WithMany(i => i.UserInterests)
            .HasForeignKey(ui => ui.InterestId);
    }
}
