using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trivo.Domain.Models;

namespace Trivo.Infrastructure.Persistence.Configurations;

public class RecruiterConfig : IEntityTypeConfiguration<Recruiter>
{
    public void Configure(EntityTypeBuilder<Recruiter> builder)
    {
        // Table Mapping
        builder.ToTable("Recruiter");

        // Primary Key
        builder.HasKey(r => r.Id)
            .HasName("PKRecruiterId");

        // Properties
        builder.Property(r => r.Id)
            .HasColumnName("PKRecruiterId")
            .IsRequired();

        builder.Property(r => r.CompanyName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.CreatedAt)
            .IsRequired();

        builder.Property(r => r.UpdatedAt)
            .IsRequired(false);

        // Foreign Key Column Name
        builder.Property(r => r.UserId)
            .HasColumnName("FKUserId")
            .IsRequired();

        // At most one Recruiter row per user — same reasoning as ExpertConfig's unique index.
        builder.HasIndex(r => r.UserId)
            .IsUnique();
    }
}