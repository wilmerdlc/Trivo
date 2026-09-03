using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Trivo.Application.Behaviors;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Services;

namespace Trivo.Application;

public static class DependencyInjection
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddScoped<IEmailValidationService, EmailValidationService>();
        services.AddScoped<INotificationService, NotificationService>();

				services.AddHttpContextAccessor();
    }
}
