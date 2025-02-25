namespace PostSys.Domain.Contracts;

/// <summary>Статусы посылки.</summary>
public enum PackageStatus
{
	/// <summary>Посылка на складе.</summary>
	OnWarehouse,

	/// <summary>Посылка в процессе доставки.</summary>
	InWork,

	/// <summary>Посылка доставлена.</summary>
	End
}
