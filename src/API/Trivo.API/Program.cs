using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
using Trivo.API.Extensions;
using Trivo.API.Filters;
using Trivo.Application;
using Trivo.Infrastructure.Persistence;
using Trivo.Infrastructure.Persistence.Context;
using Trivo.Infrastructure.Shared;
using Trivo.Infrastructure.Shared.SignalR.Hubs;

try
{
    Log.Information("Starting server");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(context.Configuration);
    });

    builder.Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();


    // Add services to the container.
    builder.Services.AddControllers(options => { options.Filters.Add<ResultFilter>(); });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });

    builder.Services.AddPersistenceLayer(builder.Configuration);
    builder.Services.AddApplicationLayer();
    builder.Services.AddInfrastructureShared(builder.Configuration);
    builder.Services.AddVersioning();

    builder.Services.AddApiHealthChecks(builder.Configuration);

    // Registration only — app.UseHsts() is intentionally not wired into the
    // pipeline yet. The Production container has no TLS in front of it today,
    // so sending this header there would tell browsers to only use HTTPS
    builder.Services.AddHsts(options =>
    {
        options.Preload = false;
        options.IncludeSubDomains = true;
        options.MaxAge = TimeSpan.FromDays(7); // start moderate, raise once verified stable behind real TLS
    });

    // Falls back to the dev origins if Cors:AllowedOrigins isn't configured, so this stays a
    // no-op for local dev. Production must set it (via CORS_ALLOWED_ORIGINS in compose.prod.yaml)
    // — the old hardcoded list only had localhost origins, which would silently reject every
    // real frontend origin in production.
    var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                          ?? ["http://127.0.0.1:5500", "http://localhost:3000", "http://localhost:3008"];

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend", policy =>
        {
            policy.WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<TrivoContext>();
        db.Database.Migrate(); // Applies pending migrations automatically
    }

    app.UseCors("AllowFrontend");

    app.UseRouting();

    app.UseWebSockets();

    app.UseSerilogRequestLogging(options =>
    {
        options.GetLevel = (httpContext, elapsed, ex) => ex is not null || httpContext.Response.StatusCode > 499
            ? LogEventLevel.Error
            : httpContext.Request.Path.StartsWithSegments("/health")
                ? LogEventLevel.Verbose
                : LogEventLevel.Information;
    });

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    {
        app.UseSwagger();
        app.MapScalarApiReference("swagger", options =>
        {
            options.WithTitle("Trivo API")
                .WithTheme(ScalarTheme.Purple)
                .WithOpenApiRoutePattern("/swagger/{documentName}.json");
        });
    }

    // The container only ever binds http:// (ASPNETCORE_URLS=http://+:5026, no
    // certificate available yet), so there's no https endpoint to redirect to
    // in Production. UseHttpsRedirection would just log a warning on every
    // request. Once a reverse proxy/load balancer terminates TLS in front of
    // it, this should be revisited alongside UseForwardedHeaders().
    if (!app.Environment.IsProduction())
    {
        app.UseHttpsRedirection();
    }

    app.UseCustomExceptionHandler();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.MapApiHealthChecks();

    app.MapHub<ChatHub>("/hubs/chat");
    app.MapHub<UserRecommendationHub>("/hubs/recommendations");
    app.MapHub<MatchHub>("/hubs/matches");
    app.MapHub<NotificationHub>("/hubs/notifications");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
    Console.Error.WriteLine(ex);
    Environment.ExitCode = 1;
}
finally
{
    Log.CloseAndFlush();
}
