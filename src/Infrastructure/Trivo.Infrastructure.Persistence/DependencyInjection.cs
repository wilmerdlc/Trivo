using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pgvector.EntityFrameworkCore;
using StackExchange.Redis;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Repository.Base;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Domain.Common;
using Trivo.Infrastructure.Persistence.Base;
using Trivo.Infrastructure.Persistence.Context;
using Trivo.Infrastructure.Persistence.Repository;
using Trivo.Infrastructure.Persistence.Repository.Account;
using Trivo.Infrastructure.Persistence.Services;

namespace Trivo.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddDbContext(configuration);
        services.AddRedis(configuration);
        services.AddRepositories(configuration);
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IGetExpertIdService, GetExpertIdService>();
        services.AddScoped<IGetRecruiterIdService, GetRecruiterIdService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TrivoContext>(postgres =>
        {
            postgres.UseNpgsql(configuration.GetConnectionString("TrivoContext"),
                b =>
                {
                    b.MigrationsAssembly("Trivo.Infrastructure.Persistence");
                    b.UseVector();
                });
        });

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<TrivoContext>());
    }

    private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient(typeof(IValidation<>), typeof(Validation<>));
        services.AddTransient<IAdministratorRepository, AdministratorRepository>();
        services.AddTransient<IExpertRepository, ExpertRepository>();
        services.AddTransient<IRecruiterRepository, RecruiterRepository>();
        services.AddTransient<ICodeRepository, CodeRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ISkillRepository, SkillRepository>();
        services.AddTransient<IUserSkillRepository, UserSkillRepository>();
        services.AddTransient<IInterestRepository, InterestRepository>();
        services.AddTransient<IUserInterestRepository, UserInterestRepository>();
        services.AddTransient<IInterestCategoryRepository, InterestCategoryRepository>();
        services.AddTransient<IMatchRepository, MatchRepository>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IGetExpertIdService, GetExpertIdService>();
        services.AddScoped<IGetRecruiterIdService, GetRecruiterIdService>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddTransient<INotificationRepository, NotificationRepository>();
        services.AddTransient<IReportRepository, ReportRepository>();
    }

    private static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Redis")!;
        services.AddStackExchangeRedisCache(options => { options.Configuration = connectionString; }
        );

        // Singleton: IConnectionMultiplexer is thread-safe and expensive to create (manages its own
        // pool of TCP connections) — registering it per-scope/transient would open and tear down
        // connections on every request.
        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(connectionString));
        services.AddSingleton<ICacheService, RedisCacheService>();
    }
}