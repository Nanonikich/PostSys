using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PostSys.Application.Extensions;
using PostSys.Dal.Common.UnitOfWork;

namespace PostSys.Service.Extensions;

/// <summary>Определяет расширения для поддержки CQRS-pattern.</summary>
public static class CqrsExtensions
{
    /// <summary>Регистрирует application-слой и pipeline UnitOfWork.</summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <param name="assemblies">Сборки с обработчиками команд (по умолчанию - application).</param>
    /// <returns><see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddCqrs(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddApplication(assemblies);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        services.AddDomainEventHandlers(typeof(CqrsExtensions).Assembly);
        return services;
    }
}