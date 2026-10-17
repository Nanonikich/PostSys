using System;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using PostSys.Application.Abstractions;

namespace PostSys.Service.Extensions;

/// <summary>Регистрация обработчиков доменных событий.</summary>
public static class DomainEventHandlerExtensions
{
	/// <summary>Регистрирует все <see cref="IDomainEventHandler{TEvent}"/> из указанной сборки.</summary>
	public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services, Assembly assembly)
	{
		ArgumentNullException.ThrowIfNull(services);
		ArgumentNullException.ThrowIfNull(assembly);

		var handlerInterface = typeof(IDomainEventHandler<>);
		foreach(var type in assembly.GetTypes().Where(t => t is { IsAbstract: false, IsInterface: false }))
		{
			foreach(var implemented in type.GetInterfaces()
						.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface))
			{
				services.AddScoped(implemented, type);
			}
		}

		return services;
	}
}
