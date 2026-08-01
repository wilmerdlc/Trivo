using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Domain.Configurations;
using Trivo.Infrastructure.Shared.Services;
using Trivo.Infrastructure.Shared.SignalR;

using Trivo.Application.DTOs.Authentication;

namespace Trivo.Infrastructure.Shared;

public static class DependencyInjection
{
    public static void AddInfrastructureShared(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddConfiguration(configuration);
        services.AddJwtAuthentication(configuration);
        services.AddAiService(configuration);
    }

    private static void AddAiService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AiSetting>(configuration.GetSection("AiSetting"));

        services.AddScoped<IAiCompletionService, OpenAiCompletionService>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<ICodeService, CodeService>();

        #region SignalR

        services.AddSignalR();

        services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
        services.AddSingleton<IAiNotifier, AiNotifier>();
        services.AddTransient<IRealTimeNotifier, RealTimeNotifier>();
        services.AddScoped<IMatchNotifier, MatchNotifier>();
        services.AddScoped<INotificationNotifier, NotificationNotifier>();

        #endregion
    }

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSetting>(configuration.GetSection("JWTConfigurations"));

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTConfigurations:Issuer"],
                    ValidAudience = configuration["JWTConfigurations:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JWTConfigurations:Key"] ?? string.Empty))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        string message = context.Exception is SecurityTokenExpiredException
                            ? "The token has expired"
                            : "Invalid token or authentication error";

                        var result = JsonConvert.SerializeObject(new JwtResponse(true, message));
                        return context.Response.WriteAsync(result);
                    },

                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var result =
                            JsonConvert.SerializeObject(new JwtResponse(true,
                                "An unexpected authentication error occurred"));
                        return context.Response.WriteAsync(result);
                    },

                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        var result = JsonConvert.SerializeObject(new JwtResponse(true,
                            "You are not authorized to access this content"));
                        return context.Response.WriteAsync(result);
                    },

                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        // Support for SignalR hubs
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }

    private static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSetting>(configuration.GetSection("EmailSetting"));
        services.Configure<CloudinarySetting>(configuration.GetSection("CloudinarySetting"));
    }
}