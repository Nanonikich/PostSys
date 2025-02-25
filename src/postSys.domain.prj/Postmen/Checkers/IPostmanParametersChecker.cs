namespace PostSys.Domain.Postmen.Checkers;

/// <summary>Представляет инструмент проверки данных почтальона.</summary>
public interface IPostmanParametersChecker
{
	#region Methods

	/// <summary>Выполняет проверку уникальности Email.</summary>
	/// <param name="email">Email.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
	/// <returns><c>true</c> если Email уникален, иначе <c>false</c>.</returns>
	Task<bool> CheckUniqueEmailAsync(string email, CancellationToken cancellationToken = default);

	/// <summary>Выполняет проверку существования посылки.</summary>
	/// <param name="packageId">Идентификатор посылки.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
	/// <returns><c>true</c> если посылка найдена, иначе <c>false</c>.</returns>
	Task<bool> CheckExistenceOfPackageAsync(Guid packageId, CancellationToken cancellationToken = default);

	/// <summary>Выполняет проверку, что посылка не находится в работе.</summary>
	/// <param name="packageId">Идентификатор посылки.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
	/// <returns><c>true</c> если посылка не в работе, иначе <c>false</c>.</returns>
	Task<bool> CheckPackageIsNotInOperationAsync(Guid packageId, CancellationToken cancellationToken = default);

	#endregion
}