namespace PostSys.Domain.Packages.Checkers;

/// <summary>Представляет инструмент проверки данных посылки.</summary>
public interface IPackageParametersChecker
{
	#region Methods

	/// <summary>Выполняет проверку существования клиента.</summary>
	/// <param name="clientId">Идентификатор клиента.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
	/// <returns><c>true</c> если клиент найден, иначе <c>false</c>.</returns>
	Task<bool> CheckExistenceOfClientAsync(Guid clientId, CancellationToken cancellationToken = default);

	/// <summary>Выполняет проверку существования почтальона.</summary>
	/// <param name="postmanId">Идентификатор почтальона.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
	/// <returns><c>true</c> если почтальон найден, иначе <c>false</c>.</returns>
	Task<bool> CheckExistenceOfPostmanAsync(Guid postmanId, CancellationToken cancellationToken = default);

	#endregion
}