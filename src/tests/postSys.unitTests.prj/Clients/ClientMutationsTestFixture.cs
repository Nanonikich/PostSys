using System;

using CSharpFunctionalExtensions;

using NUnit.Framework;

using PostSys.Domain.Clients;
using PostSys.Domain.ValueObjects;

namespace PostSys.UnitTests.Clients;

/// <summary>Определяет тесты для проверки действий над сущностью "Клиент".</summary>
[TestFixture]
public class ClientMutationsTestFixture
{
	[Test]
	[Description("Проверяет создание клиента без данных об имени.")]
	public void Create_client_without_fullname_impossible()
	{
		// Arrange
		var phoneNumber = PhoneNumber.Create("8953-522-66-88");

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => Client.Create(
			null,
			phoneNumber.Value));
	}

	[Test]
	[Description("Проверяет изменение данных об имени клиента.")]
	public void Change_client_fullname_success()
	{
		// Arrange
		var client = ClientTestHelper.CreateClient();
		var newFullname = Fullname.Create("Иванов", "Андрей", "Андреевич");

		// Act & Assert
		Assert.DoesNotThrow(() => client.ChangeFullname(newFullname.Value));
	}

	[Test]
	[Description("Проверяет изменение номера телефона клиента.")]
	public void Change_client_phone_number_success()
	{
		// Arrange
		var client = ClientTestHelper.CreateClient();
		var newPhoneNumber = PhoneNumber.Create("8933-655-28-71");

		// Act & Assert
		Assert.DoesNotThrow(() => client.ChangePhoneNumber(newPhoneNumber.Value));
	}

	[Test]
	[Description("Проверяет изменение номера телефона клиента на некорректный номер.")]
	public void Change_client_phone_number_to_incorrect_impossible()
	{
		// Arrange
		var client = ClientTestHelper.CreateClient();
		var newPhoneNumber = PhoneNumber.Create("111");

		// Act & Assert
		Assert.Throws<ResultFailureException>(() => client.ChangePhoneNumber(newPhoneNumber.Value));
	}
}