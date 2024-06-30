using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Serilog;

using PostSys.Application.Context;
using PostSys.Models;

namespace PostSys.Application.Views.Controls;

/// <summary>Элемент управления, содержащий таблицу отправителей.</summary>
public partial class DgvSendersControl : BaseDgvControl
{
	private static readonly ILogger Log = Serilog.Log.ForContext<DgvSendersControl>();

	private string _searchText;

	/// <summary>Событие установки режима поиска по тексту.</summary>
	public EventHandler<string> TextSearchChanged = delegate { };

	/// <summary>Все данные в таблице.</summary>
	public IReadOnlyList<Sender> AllTableData { get; protected set; }

	/// <summary>Создаёт экземпляр класса <see cref="DgvSendersControl"/>.</summary>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	public DgvSendersControl(PostSysContext dbContext) : base()
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

		AllTableData = [.. DbContext.Sender
			.Include(x => x.SenderCityNavigation)
			.Include(x => x.SenderStreetNavigation.AddressCodeStreetNavigation)];

		if(_searchText.IsNullOrEmpty())
		{
			DataGrid.DataSource = AllTableData.Select(x => new
			{
				ID = x.SenderId,
				Фамилия = x.SenderSurname,
				Имя = x.SenderName,
				Отчество = x.SenderPatronymic,
				Город = x.SenderCityNavigation.CityName,
				Улица = x.SenderStreetNavigation.AddressCodeStreetNavigation.StreetName,
				Дом = x.SenderHome,
				Квартира = x.SenderApartment,
				Телефон = x.SenderPhone,
			}).ToList();
		}
		else
		{
			DataGrid.DataSource = AllTableData.Where(x => x.SenderSurname.Contains(_searchText, StringComparison.CurrentCultureIgnoreCase))
				.Select(x => new
				{
					ID = x.SenderId,
					Фамилия = x.SenderSurname,
					Имя = x.SenderName,
					Отчество = x.SenderPatronymic,
					Город = x.SenderCityNavigation.CityName,
					Улица = x.SenderStreetNavigation.AddressCodeStreetNavigation.StreetName,
					Дом = x.SenderHome,
					Квартира = x.SenderApartment,
					Телефон = x.SenderPhone,
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
				DbContext.Remove(AllTableData.First(x => x.SenderId == (int)CurrentRow.Cells[0].Value));
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

	private void OnTextSearchChanged(object? sender, string e)
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
