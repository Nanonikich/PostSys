using Serilog;

using PostSys.Application.Context;
using PostSys.Models;

namespace PostSys.Application.Views.Controls;

/// <summary>Элемент управления, содержащий таблицу улиц.</summary>
public partial class DgvStreetsControl : BaseDgvControl
{
	private static readonly ILogger Log = Serilog.Log.ForContext<DgvStreetsControl>();

	/// <summary>Все данные в таблице.</summary>
	public IReadOnlyList<Street> AllTableData { get; protected set; }

	/// <summary>Создаёт экземпляр класса <see cref="DgvStreetsControl"/>.</summary>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	public DgvStreetsControl(PostSysContext dbContext) : base()
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

		AllTableData = [.. DbContext.Street];

		DataGrid.DataSource = AllTableData.Select(x => new
		{
			ID = x.StreetId,
			Улица = x.StreetName,
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
				DbContext.Remove(AllTableData.First(x => x.StreetId == (int)CurrentRow.Cells[0].Value));
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
}
