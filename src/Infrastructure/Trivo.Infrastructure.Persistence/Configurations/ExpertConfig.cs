using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trivo.Domain.Models;

namespace Trivo.Infrastructure.Persistence.Configurations;

public class ExpertConfig : IEntityTypeConfiguration<Expert>
{
    public void Configure(EntityTypeBuilder<Expert> builder)
    {
        // Table Mapping
        builder.ToTable("Expert");

        // Primary Key
        builder.HasKey(e => e.Id)
            .HasName("PKExpertId");

        // Properties
        builder.Property(e => e.Id)
            .HasColumnName("PKExpertId")
            .IsRequired();

        builder.Property(e => e.AvailableForProjects)
            .IsRequired();

        builder.Property(e => e.IsHired)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.UpdatedAt)
            .IsRequired(false);

        // Foreign Key Column Name
        builder.Property(e => e.UserId)
            .HasColumnName("FKUserId")
            .IsRequired();

        // At most one Expert row per user — GetUserRecommendationsQueryHandler's
        // ToDictionary(e => e.UserId) assumes this, and would throw otherwise.
        builder.HasIndex(e => e.UserId)
            .IsUnique();
    }
}