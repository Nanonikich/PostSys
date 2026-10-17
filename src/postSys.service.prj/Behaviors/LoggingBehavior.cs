using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

namespace PostSys.Service.Common.Behaviors;

/// <summary>
/// Определяет behavior of pipeline для логирования обработки запроса <typeparamref name="TRequest"/>.
/// </summary>
/// <typeparam name="TRequest">Тип запроса.</typeparam>
/// <typeparam name="TResponse">Тип возвращаемого ответа.</typeparam>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest
{
	#region Data

	private readonly ILogger _logger;

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="LoggingBehavior{TRequest, TResponse}"/>.</summary>
	/// <param name="logger">Логгер.</param>
	/// <exception cref="ArgumentNullException">Значение <paramref name="logger"/> не определено.</exception>
	public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
	{
		ArgumentNullException.ThrowIfNull(logger, nameof(logger));
		_logger = logger;
	}

	#endregion

	#region IPipelineBehavior<TRequest,TResponse> members

	/// <inheritdoc/>
	public async Task<TResponse> Handle(
		TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		TResponse response1;
		using(_logger.BeginScope(new Dictionary<string, object>
		{
			{ "Request", request }
		}))
		{
			try
			{
				_logger.LogInformation("Processing request {RequestName}...", typeof(TRequest).Name);
				var response2 = await next();
				_logger.LogInformation("Request {RequestName} successfully processed", typeof(TRequest).Name);
				response1 = response2;
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Request processing error {RequestName}:", typeof(TRequest).Name);
				throw;
			}
		}

		return response1;
	}

	#endregion
}