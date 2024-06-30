using Microsoft.IdentityModel.Tokens;

using Serilog;

using PostSys.Application.Context;
using PostSys.Models;

namespace PostSys.Application.Views.Controls;

/// <summary>Элемент управления, содержащий таблицу почтальонов.</summary>
public partial class DgvPostmansControl : BaseDgvControl
{
	private static readonly ILogger Log = Serilog.Log.ForContext<DgvPostmansControl>();

	private string _searchText;

	/// <summary>Все данные в таблице.</summary>
	public IReadOnlyList<Postman> AllTableData { get; protected set; }

	/// <summary>Событие установки режима поиска по тексту.</summary>
	public EventHandler<string> TextSearchChanged = delegate { };

	/// <summary>Создаёт экземпляр класса <see cref="DgvPostmansControl"/>.</summary>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	public DgvPostmansControl(PostSysContext dbContext) : base()
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

		AllTableData = [.. DbContext.Postman];

		if(!_searchText.IsNullOrEmpty() && int.TryParse(_searchText, out int searchedPlot))
		{
			DataGrid.DataSource = AllTableData.Where(x => x.PostmanPlot == searchedPlot)
				.Select(x => new
				{
					ID = x.PostmanId,
					Фамилия = x.PostmanSurname,
					Имя = x.PostmanName,
					Отчество = x.PostmanPatronymic,
					Телефон = x.PostmanPhone,
					Участок = x.PostmanPlot,
				}).ToList();
		}
		else
		{
			DataGrid.DataSource = AllTableData.Select(x => new
			{
				ID = x.PostmanId,
				Фамилия = x.PostmanSurname,
				Имя = x.PostmanName,
				Отчество = x.PostmanPatronymic,
				Телефон = x.PostmanPhone,
				Участок = x.PostmanPlot,
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
				DbContext.Remove(AllTableData.First(x => x.PostmanId == (int)CurrentRow.Cells[0].Value));
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
