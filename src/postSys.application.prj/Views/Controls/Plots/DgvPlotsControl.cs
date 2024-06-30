using Microsoft.EntityFrameworkCore;

using Serilog;

using PostSys.Application.Context;
using PostSys.Models;

namespace PostSys.Application.Views.Controls;

/// <summary>Элемент управления, содержащий таблицу участков.</summary>
public partial class DgvPlotsControl : BaseDgvControl
{
	private static readonly ILogger Log = Serilog.Log.ForContext<DgvPlotsControl>();

	/// <summary>Все данные в таблице.</summary>
	public IReadOnlyList<AddressCode> AllTableData { get; protected set; }

	/// <summary>Создаёт экземпляр класса <see cref="DgvUsersControl"/>.</summary>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	public DgvPlotsControl(PostSysContext dbContext) : base()
	{
		DbContext = dbContext;

		InitializeComponent();
	}

	#region Methods

	/// <summary>Загрузка данных в таблицу.</summary>
	public override void LoadingTableData()
	{
		if(CurrentRow != null)
			CurrentId = (int)CurrentRow.Cells[0].Value;

		AllTableData = [.. DbContext.AddressCode
			.Include(x => x.AddressCodeCityNavigation)
			.Include(x => x.AddressCodeStreetNavigation)];

		DataGrid.DataSource = AllTableData.Select(x => new
		{
			ID = x.AddressCodeId,
			Участок = x.AddressCodePlot,
			Город = x.AddressCodeCityNavigation.CityName,
			Улица = x.AddressCodeStreetNavigation.StreetName,
			Дома = x.AddressCodeHouses
		}).ToList();

		DataGrid.Columns[0].Visible = false;

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
				DbContext.Remove(AllTableData.First(x => x.AddressCodeId == (int)DataGrid.CurrentRow.Cells[0].Value));
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

}
