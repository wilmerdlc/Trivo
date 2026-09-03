using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Domain.Models;

namespace Trivo.Infrastructure.Persistence.Context;

public class TrivoContext(DbContextOptions<TrivoContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Administrator> Administrators => Set<Administrator>();
    public DbSet<Expert> Experts => Set<Expert>();
    public DbSet<Recruiter> Recruiters => Set<Recruiter>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<UserSkill> UserSkills => Set<UserSkill>();
    public DbSet<Interest> Interests => Set<Interest>();
    public DbSet<InterestCategory> InterestCategories => Set<InterestCategory>();
    public DbSet<UserInterest> UserInterests => Set<UserInterest>();
    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<ChatUser> ChatUsers => Set<ChatUser>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Report> Reports => Set<Report>();
    public DbSet<Code> Codes => Set<Code>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }
}