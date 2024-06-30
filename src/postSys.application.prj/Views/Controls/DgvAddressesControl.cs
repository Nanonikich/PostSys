using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Serilog;

using PostSys.Application.Context;
using PostSys.Models;

namespace PostSys.Application.Views.Controls;

/// <summary>Элемент управления, содержащий таблицу адресов.</summary>s
public partial class DgvAddressesControl : BaseDgvControl
{
	private static readonly ILogger Log = Serilog.Log.ForContext<DgvAddressesControl>();

	private (string Recipient, string Postman) _searchText;

	/// <summary>Событие установки режима поиска по тексту.</summary>
	public EventHandler<(string Recipient, string Postman)> TextSearchChanged = delegate { };

	/// <summary>Все данные в таблице.</summary>
	public IReadOnlyList<Address> AllTableData { get; protected set; }

	/// <summary>Создаёт экземпляр класса <see cref="DgvUsersControl"/>.</summary>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	public DgvAddressesControl(PostSysContext dbContext) : base()
	{
		DbContext = dbContext;
		TextSearchChanged += OnTextSearchChanged;

		InitializeComponent();
	}

	#region Methods

	/// <summary>Загрузка данных в таблицу.</summary>
	public override void LoadingTableData()
	{
		if(CurrentRow != null)
			CurrentId = (int)CurrentRow.Cells[0].Value;

		AllTableData = [.. DbContext.Address
			.Include(x => x.AddressPostmanNavigation)
			.Include(x => x.AddressRecipientNavigation.RecipientCityNavigation)
			.Include(x => x.AddressRecipientNavigation.RecipientStreetNavigation.AddressCodeStreetNavigation)];

		if(_searchText.Recipient.IsNullOrEmpty() && _searchText.Postman.IsNullOrEmpty())
		{
			DataGrid.DataSource = AllTableData.Select(x => new
			{
				ID = x.AddressId,
				Участок = x.AddressPostmanNavigation.PostmanPlot,
				Получатель = x.AddressRecipientNavigation.RecipientSurname,
				Город = x.AddressRecipientNavigation.RecipientCityNavigation.CityName,
				Улица = x.AddressRecipientNavigation.RecipientStreetNavigation.AddressCodeStreetNavigation.StreetName,
				Дом = x.AddressHome,
				Квартира = x.AddressApartment,
				Почтальон = x.AddressPostmanNavigation.PostmanSurname,
				Товары = x.AddressGoods,
			}).ToList();
		}
		else
		{
			DataGrid.DataSource = AllTableData
				.Where(s => s.AddressRecipientNavigation.RecipientSurname.Contains(_searchText.Recipient, StringComparison.CurrentCultureIgnoreCase) &&
							s.AddressPostmanNavigation.PostmanSurname.Contains(_searchText.Postman, StringComparison.CurrentCultureIgnoreCase))
				.Select(x => new
				{
					ID = x.AddressId,
					Участок = x.AddressPostmanNavigation.PostmanPlot,
					Получатель = x.AddressRecipientNavigation.RecipientSurname,
					Город = x.AddressRecipientNavigation.RecipientCityNavigation.CityName,
					Улица = x.AddressRecipientNavigation.RecipientStreetNavigation.AddressCodeStreetNavigation.StreetName,
					Дом = x.AddressHome,
					Квартира = x.AddressApartment,
					Почтальон = x.AddressPostmanNavigation.PostmanSurname,
					Товары = x.AddressGoods
				}).ToList();
		}

		ShadingCurrentRow();
	}

	/// <summary>Удаление выбранного ряда таблицы.</summary>
	public void DeleteCurrentRow()
	{
		if(CurrentRow != null)
		{
			Timer.Stop();

			try
			{
				DbContext.Remove(AllTableData.First(x => x.AddressId == (int)CurrentRow.Cells[0].Value));
				DbContext.SaveChanges();
			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				MessageBox.Show($"Произошла ошибка. Не удалось удалить данные.");
			}

			Timer.Start();
		}
		else
		{
			MessageBox.Show("Нет пользователей.");
		}
	}

	#endregion

	#region Handlers

	private void OnTextSearchChanged(object? sender, (string Recipient, string Postman) e)
	{
		Timer.Stop();

		_searchText = e;

		Timer.Start();
	}

	#endregion

	#region DisposableEx

	public override void DisposeEx()
	{
		if(!IsDisposed)
		{
			base.DisposeEx();

			TextSearchChanged -= OnTextSearchChanged;
		}
	}

	#endregion
}
