using HotChocolate.Types;

namespace PostSys.Service.Common.GraphQL.Attributes;

/// <summary>Представляет атрибут, указывающий на использование конвенции мутации с успешным результатом.</summary>
public sealed class UseSuccessMutationConventionAttribute : UseMutationConventionAttribute
{
	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="UseSuccessMutationConventionAttribute"/>.</summary>
	public UseSuccessMutationConventionAttribute() => PayloadFieldName = "isSuccess";

	#endregion
}