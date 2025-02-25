using System;

namespace PostSys.Client;

/// <summary>Исключения для клиента GraphQL.</summary>
public class GraphQlClientException : Exception
{
	#region .ctor

	/// <summary>Исключения для клиента GraphQL.</summary>
	/// <param name="message">Сообщение об исключении.</param>
	public GraphQlClientException(string message) : base(message) { }

	/// <summary>Исключения для клиента GraphQL.</summary>
	/// <param name="message">Сообщение об исключении.</param>
	/// <param name="innerException">Вложенное исключение.</param>
	public GraphQlClientException(string message, Exception innerException) : base(message, innerException) { }

	#endregion
}
