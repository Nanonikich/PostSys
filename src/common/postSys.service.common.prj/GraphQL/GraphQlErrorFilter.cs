using System;

using HotChocolate;

using PostSys.Service.Common.Exceptions;

namespace PostSys.Service.Common.GraphQL;

/// <summary>Фильтр ошибок GraphQL.</summary>
public class GraphQlErrorFilter : IErrorFilter
{
	#region Methods

	/// <inheritdoc/>
	public IError OnError(IError error)
	{
		if(error.Exception is ArgumentException || error.Exception is ArgumentNullException)
		{
			return ErrorBuilder
				.New()
				.SetMessage(error.Exception.Message)
				.SetCode("INVALID_ARGUMENT")
				.Build();
		}
		else if(error.Exception is EntityNotFoundException)
		{
			return ErrorBuilder
				.New()
				.SetMessage(error.Exception.Message)
				.SetCode("ENTITY_NOT_FOUND")
				.Build();
		}

		return ErrorBuilder
			.New()
			.SetMessage(error.Exception!.Message)
			.SetCode("UNKNOWN_ERROR")
			.Build();
	}

	#endregion
}