using Asp.Versioning;
using Trivo.API.Middlewares;

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
}
