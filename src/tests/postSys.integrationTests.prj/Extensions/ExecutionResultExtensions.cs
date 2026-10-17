using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;

using HotChocolate.Execution;

using NUnit.Framework;

namespace PostSys.IntegrationTests.Common.Extensions;

/// <summary>Определяет методы расширения для <see cref="IExecutionResult" />.</summary>
public static class ExecutionResultExtensions
{
	#region Methods

	/// <summary>Проверяет, что результат выполнения запроса не содержит ошибок.</summary>
	/// <param name="result">Результат выполнения запроса.</param>
	public static void AssertErrors(this IExecutionResult result)
	{
		var operationResult = result.ExpectOperationResult();
		if(operationResult.Errors is null)
			return;

		var builder = new StringBuilder("GraphQL query errors:");
		foreach(var error in operationResult.Errors)
			builder.AppendLine().Append(error.Message);

		Assert.Fail(builder.ToString());
	}

	/// <summary>Возвращает идентификатор из результата выполнения запроса.</summary>
	/// <param name="result">Результат выполнения запроса.</param>
	/// <param name="key">Ключ, по которому необходимо получить идентификатор.</param>
	/// <typeparam name="T">Тип возвращаемого идентификатора.</typeparam>
	/// <returns>Идентификатор.</returns>
	public static T GetIdFromResult<T>(this IExecutionResult result, string key)
	{
		result.AssertErrors();

		var data = result.ExpectOperationResult().Data;
		if(data is null || data.Value.Value is null)
			throw new InvalidOperationException($"Идентификатор по ключу '{key}' не найден.");

		using var document = JsonDocument.Parse(JsonSerializer.Serialize(data.Value.Value));
		if(!document.RootElement.TryGetProperty(key, out var node))
			throw new InvalidOperationException($"Идентификатор по ключу '{key}' не найден.");

		var idString = node.ValueKind switch
		{
			JsonValueKind.Object when node.TryGetProperty("id", out var idProperty) => idProperty.GetString(),
			JsonValueKind.String => node.GetString(),
			_ => node.ToString()
		};

		if(string.IsNullOrEmpty(idString))
			throw new InvalidOperationException($"Идентификатор по ключу '{key}' не найден.");

		var id = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(idString)!;
		ValidateConvertedId(id);
		return id;
	}

	/// <summary>Проверка валидности идентификатора.</summary>
	/// <param name="id">Идентификатор.</param>
	/// <typeparam name="T">Тип возвращаемого идентификатора.</typeparam>
	/// <exception cref="InvalidOperationException">Исключение при невалидности.</exception>
	private static void ValidateConvertedId<T>(T id)
	{
		if(EqualityComparer<T>.Default.Equals(id, default))
			throw new InvalidOperationException($"Идентификатор имеет значение по умолчанию: {id}");
	}

	#endregion
}
