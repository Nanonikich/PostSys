using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Serilog;

using PostSys.Application.Context;
using PostSys.Models;

namespace PostSys.Application.Views.Controls;

/// <summary>Элемент управления, содержащий таблицу получателей.</summary>
public partial class DgvRecipientsControl : BaseDgvControl
{
	private static readonly ILogger Log = Serilog.Log.ForContext<DgvRecipientsControl>();

	private (string Sender, string Recipient) _searchText;

	/// <summary>Событие установки режима поиска по тексту.</summary>
	public EventHandler<(string Sender, string Recipient)> TextSearchChanged = delegate { };

	/// <summary>Все данные в таблице.</summary>
	public IReadOnlyList<Recipient> AllTableData { get; protected set; }

	/// <summary>Создаёт экземпляр класса <see cref="DgvRecipientsControl"/>.</summary>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	public DgvRecipientsControl(PostSysContext dbContext) : base()
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

		AllTableData = [.. DbContext.Recipient
			.Include(x => x.RecipientCityNavigation)
			.Include(x => x.RecipientStreetNavigation.AddressCodeStreetNavigation)
			.Include(x => x.RecipientSenderNavigation)];

		if(_searchText.Sender.IsNullOrEmpty() && _searchText.Recipient.IsNullOrEmpty())
		{
			DataGrid.DataSource = AllTableData.Select(x => new
			{
				ID = x.RecipientId,
				Серия = x.RecipientSeries,
				Номер = x.RecipientNumber,
				Фамилия = x.RecipientSurname,
				Имя = x.RecipientName,
				Отчество = x.RecipientPatronymic,
				Город = x.RecipientCityNavigation.CityName,
				Улица = x.RecipientStreetNavigation.AddressCodeStreetNavigation.StreetName,
				Дом = x.RecipientHome,
				Квартира = x.RecipientApartment,
				Телефон = x.RecipientPhone,
				Отправитель = x.RecipientSenderNavigation.SenderSurname,
			}).ToList();
		}
		else
		{
			DataGrid.DataSource = AllTableData
				.Where(x => x.RecipientSenderNavigation.SenderSurname.Contains(_searchText.Sender, StringComparison.CurrentCultureIgnoreCase) &&
							x.RecipientSurname.Contains(_searchText.Recipient, StringComparison.CurrentCultureIgnoreCase))
				.Select(x => new
				{
					ID = x.RecipientId,
					Серия = x.RecipientSeries,
					Номер = x.RecipientNumber,
					Фамилия = x.RecipientSurname,
					Имя = x.RecipientName,
					Отчество = x.RecipientPatronymic,
					Город = x.RecipientCityNavigation.CityName,
					Улица = x.RecipientStreetNavigation.AddressCodeStreetNavigation.StreetName,
					Дом = x.RecipientHome,
					Квартира = x.RecipientApartment,
					Телефон = x.RecipientPhone,
					Отправитель = x.RecipientSenderNavigation.SenderSurname,
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
				DbContext.Remove(AllTableData.First(x => x.RecipientId == (int)CurrentRow.Cells[0].Value));
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
			MessageBox.Show("В таблице нет данных.");
		}
	}

	#endregion

	#region Handlers

	private void OnTextSearchChanged(object? sender, (string Sender, string Recipient) e)
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
