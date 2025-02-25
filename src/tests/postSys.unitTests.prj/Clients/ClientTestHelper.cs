using PostSys.Domain.Clients;
using PostSys.Domain.ValueObjects;

namespace PostSys.UnitTests.Clients;

/// <summary>Вспомогательный класс для тестов сущности "Клиент".</summary>
internal static class ClientTestHelper
{
	#region Methods

	/// <summary>Создание клиента для теста.</summary>
	/// <returns>Клиент.</returns>
	public static Client CreateClient()
	{
		var fullname = Fullname.Create("Андреев", "Андрей", "Андреевич");
		var phoneNumber = PhoneNumber.Create("8911-533-22-66");
		return Client.Create(fullname.Value, phoneNumber.Value);
	}

	#endregion
}