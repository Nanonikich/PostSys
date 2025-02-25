using HotChocolate;

namespace PostSys.ReadModels.Contracts;

/// <summary>Статусы посылки.</summary>
public enum PackageStatus
{
	/// <summary>Посылка на складе.</summary>
	[GraphQLName("OnWarehouse")]
	OnWarehouse,

	/// <summary>Посылка в процессе доставки.</summary>
	[GraphQLName("InWork")]
	InWork,

	/// <summary>Посылка доставлена.</summary>
	[GraphQLName("End")]
	End
}
