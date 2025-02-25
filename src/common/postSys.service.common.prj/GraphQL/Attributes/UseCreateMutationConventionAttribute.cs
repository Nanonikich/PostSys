using HotChocolate.Types;

namespace PostSys.Service.Common.GraphQL.Attributes;

/// <summary>Представляет атрибут, указывающий на использование конвенции мутации для создания сущности.</summary>
public sealed class UseCreateMutationConventionAttribute : UseMutationConventionAttribute
{
	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="UseCreateMutationConventionAttribute"/>.</summary>
	public UseCreateMutationConventionAttribute() => PayloadFieldName = "id";

	#endregion
}