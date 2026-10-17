using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace PostSys.Dal.Common.UnitOfWork;

/// <summary>Определяет behavior of pipeline для фиксации транзакции.</summary>
/// <typeparam name="TRequest">Тип запроса.</typeparam>
/// <typeparam name="TResponse">Тип возвращаемого ответа.</typeparam>
public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
	#region Data

	private readonly IUnitOfWork _unitOfWork;

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="UnitOfWorkBehavior{TRequest, TResponse}"/>.</summary>
	/// <param name="unitOfWork"><see cref="IUnitOfWork"/>.</param>
	/// <exception cref="ArgumentNullException">Значение <paramref name="unitOfWork"/> не определено.</exception>
	public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
	{
		ArgumentNullException.ThrowIfNull(unitOfWork, nameof(unitOfWork));
		_unitOfWork = unitOfWork;
	}

	#endregion

	#region Methods

	/// <inheritdoc/>
	public async Task<TResponse> Handle(
		TRequest request, 
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		try
		{
			var response = await next();
			await _unitOfWork.CommitAsync(cancellationToken);
			return response;
		}
		catch(Exception ex)
		{
			ExceptionDispatchInfo
				.Capture(ex)
				.Throw();
			throw;
		}
	}

	#endregion
}