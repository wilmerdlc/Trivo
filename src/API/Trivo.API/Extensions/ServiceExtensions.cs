using System.Text.Json;
using Asp.Versioning;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Trivo.API.Middlewares;
using Trivo.Infrastructure.Persistence.Context;

namespace Trivo.API.Extensions;

public static class ServiceExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static void AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true; // When no version is sent, this assumes the default version, which is V1
            options.ReportApiVersions = true;
        });
    }

    public static void AddApiHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<TrivoContext>(name: "postgresql")
            .AddRedis(configuration.GetConnectionString("Redis")!, name: "redis");
    }

    public static void MapApiHealthChecks(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";

                var payload = new
                {
                    status = report.Status.ToString(),
                    totalDurationMs = report.TotalDuration.TotalMilliseconds,
                    checks = report.Entries.Select(entry => new
                    {
                        name = entry.Key,
                        status = entry.Value.Status.ToString(),
                        description = entry.Value.Description,
                        durationMs = entry.Value.Duration.TotalMilliseconds
                    })
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
            }
        });
    }
}
