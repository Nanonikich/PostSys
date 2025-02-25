using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using HotChocolate.Execution;
using HotChocolate.Execution.Processing;
using HotChocolate.Execution.Serialization;

using NUnit.Framework;

namespace PostSys.IntegrationTests.Common.Extensions;

/// <summary>Определяет методы расширения для <see cref="IExecutionResult" />.</summary>
public static class ExecutionResultExtensions
{
	#region Static

	private static readonly JsonResultFormatter Formatter = new();

	#endregion

	#region Methods

	/// <summary>Проверяет, что результат выполнения запроса не содержит ошибок.</summary>
	/// <param name="result">Результат выполнения запроса.</param>
	public static void AssertErrors(this IExecutionResult result)
	{
		var operationResult = result.ExpectOperationResult();
		if(operationResult.Errors != null) Assert.Fail("GraphQL query errors:\r\n" + Formatter.Format(operationResult));
	}

	/// <summary>Возвращает идентификатор из результата выполнения запроса.</summary>
	/// <param name="result">Результат выполнения запроса.</param>
	/// <param name="key">Ключ, по которому необходимо получить идентификатор.</param>
	/// <typeparam name="T">Тип возвращаемого идентификатора.</typeparam>
	/// <returns>Идентификатор.</returns>
	public static T GetIdFromResult<T>(this IExecutionResult result, string key)
	{
		result.AssertErrors();

		if(!result.ExpectOperationResult().Data.TryGetValue(key, out var value) ||
		   value is not IEnumerable<ObjectFieldResult> fields)
			throw new InvalidOperationException($"Идентификатор по ключу '{key}' не найден.");

		var idString = fields.FirstOrDefault(field => field.Name == "id")?.Value as string;

		if(string.IsNullOrEmpty(idString))
			throw new InvalidOperationException($"Идентификатор по ключу '{key}' не найден.");

		var id = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(idString);

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