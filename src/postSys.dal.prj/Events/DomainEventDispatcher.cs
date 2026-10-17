using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using PostSys.Application.Abstractions;
using PostSys.Domain.Abstractions;

namespace PostSys.Dal.Events;

/// <summary>Диспетчер доменных событий через зарегистрированные <see cref="IDomainEventHandler{TEvent}"/>.</summary>
public sealed class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
	/// <inheritdoc/>
	public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(domainEvents);

		foreach(var domainEvent in domainEvents)
		{
			var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
			foreach(var handler in serviceProvider.GetServices(handlerType))
			{
				var method = handlerType.GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
					?? throw new InvalidOperationException($"HandleAsync not found on {handlerType}.");
				await (Task)method.Invoke(handler, [domainEvent, cancellationToken])!;
			}
		}
	}
}
