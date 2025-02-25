namespace PostSys.Gui.Common.Helpers;

/// <summary>Текст сообщения.</summary>
public record MessageContent
{
	#region Properties

	/// <summary>Возвращает заголовок сообщения.</summary>
	/// <value>Заголовок сообщения.</value>
	public string Title { get; init; }

	/// <summary>Возвращает контент сообщения.</summary>
	/// <value>Контент сообщения.</value>
	public string Message { get; init; }

	#endregion
}