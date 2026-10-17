using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PostSys.Application.Behaviors;

namespace PostSys.Application.Extensions;

/// <summary>Расширения регистрации application-слоя.</summary>
public static class ApplicationServiceCollectionExtensions
{
    /// <summary>Регистрирует MediatR и pipeline behaviors application-слоя.</summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <param name="assemblies">Сборки для сканирования обработчиков.</param>
    public static IServiceCollection AddApplication(this IServiceCollection services, params Assembly[] assemblies)
    {
        var toScan = assemblies.Length == 0 ? [typeof(ApplicationServiceCollectionExtensions).Assembly] : assemblies;
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(toScan);
        });
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        return services;
    }
}