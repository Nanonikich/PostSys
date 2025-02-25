using System;

using ReactiveUI;

using PostSys.Gui.Common.ViewModels;
using PostSys.ReadModels.Helpers;

namespace PostSys.Gui.Helpers;

/// <summary>Модель представления заказчика.</summary>
public sealed class ClientOfOrderHelper : ViewModelBase
{
	#region Data

	private Fullname _fullname;
	private string _phoneNumber;

	#endregion

	#region Properties

	/// <summary>Возвращает идентификатор клиента.</summary>
	/// <value>Идентификатор клиента.</value>
	public Guid Id { get; init; }

	/// <summary>Возвращает или задаёт данные об имени клиента.</summary>
	/// <value>Данные об имени клиента.</value>
	public Fullname Fullname
	{
		get => _fullname;
		set => this.RaiseAndSetIfChanged(ref _fullname, value);
	}

	/// <summary>Возвращает или задаёт номер телефона клиента.</summary>
	/// <value>Номер телефона клиента.</value>
	public string PhoneNumber
	{
		get => _phoneNumber;
		set => this.RaiseAndSetIfChanged(ref _phoneNumber, value);
	}

	#endregion
}