using Microsoft.EntityFrameworkCore;
using Serilog;
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

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontendDev", policy =>
        {
            policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:3000", "http://localhost:3008")
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

    app.UseCors("AllowFrontendDev");

    app.UseRouting();

    app.UseWebSockets();

    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            c.RoutePrefix = "swagger";
        });
    }

    app.UseHttpsRedirection();

    app.UseCustomExceptionHandler();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.MapHub<ChatHub>("/hubs/chat");
    app.MapHub<UserRecommendationHub>("/hubs/recommendations");
    app.MapHub<MatchHub>("/hubs/matches");
    app.MapHub<NotificationHub>("/hubs/notifications");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "An error has occurred");
}
finally
{
    Log.CloseAndFlush();
}
