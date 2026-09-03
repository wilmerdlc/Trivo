using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trivo.Domain.Models;

namespace Trivo.Infrastructure.Persistence.Configurations;

public class UserSkillConfig : IEntityTypeConfiguration<UserSkill>
{
    public void Configure(EntityTypeBuilder<UserSkill> builder)
    {
        // Table Mapping
        builder.ToTable("UserSkill");

        // Composite Primary Key
        builder.HasKey(us => new { us.UserId, us.SkillId });

        // Properties
        builder.Property(us => us.UserId)
            .HasColumnName("FKUserId")
            .IsRequired();

        builder.Property(us => us.SkillId)
            .HasColumnName("FKSkillId")
            .IsRequired();

        // Relationships
        builder.HasOne(us => us.User)
            .WithMany(u => u.UserSkills)
            .HasForeignKey(us => us.UserId);

        builder.HasOne(us => us.Skill)
            .WithMany(s => s.UserSkills)
            .HasForeignKey(us => us.SkillId);
    }
}
