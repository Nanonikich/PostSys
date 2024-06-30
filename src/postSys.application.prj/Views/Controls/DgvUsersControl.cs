using Microsoft.IdentityModel.Tokens;

using Serilog;

using PostSys.Application.Context;
using PostSys.Models;

namespace PostSys.Application.Views.Controls;

/// <summary>Элемент управления, содержащий таблицу пользователей системы.</summary
public partial class DgvUsersControl
{
	private static readonly ILogger Log = Serilog.Log.ForContext<DgvUsersControl>();

	private string _searchText;

	/// <summary>Событие установки режима поиска по тексту.</summary>
	public EventHandler<string> TextSearchChanged = delegate { };

	/// <summary>Все данные в таблице.</summary>
	public IReadOnlyList<User> AllTableData { get; protected set; }

	/// <summary>Создаёт экземпляр класса <see cref="DgvUsersControl"/>.</summary>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	public DgvUsersControl(PostSysContext dbContext) : base()
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

		AllTableData = [.. DbContext.Users];

		if(_searchText.IsNullOrEmpty())
		{
			DataGrid.DataSource = AllTableData.Select(x => new
			{
				ID = x.UserId,
				Фамилия = x.UserSurname,
				Имя = x.UserName,
				Отчество = x.UserPatronymic,
				Email = x.UserEmail,
				Телефон = x.UserPhone,
				Участок = x.UserCity,
				Логин = x.UserUsername,
				Пароль = x.UserPassword,
			}).ToList();
		}
		else
		{
			DataGrid.DataSource = AllTableData.Where(x => x.UserSurname.Contains(_searchText, StringComparison.CurrentCultureIgnoreCase))
				.Select(x => new
				{
					ID = x.UserId,
					Фамилия = x.UserSurname,
					Имя = x.UserName,
					Отчество = x.UserPatronymic,
					Email = x.UserEmail,
					Телефон = x.UserPhone,
					Участок = x.UserCity,
					Логин = x.UserUsername,
					Пароль = x.UserPassword,
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
				var statuses = DbContext.Role.ToList();

				var user = AllTableData.First(x => x.UserId == (int)CurrentRow.Cells[0].Value);

				if(statuses.First(x => user.UserRole == x.RoleId).RoleName != "Admin")
				{
					DbContext.Remove(user);
					DbContext.SaveChanges();
				}
				else
				{
					MessageBox.Show("Нельзя удалить администратора!");
				}
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
