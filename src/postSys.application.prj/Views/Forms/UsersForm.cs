using System.Data;

using PostSys.Application.Views.Forms.EditingForms;
using PostSys.Models;
using PostSys.Application.ViewModels;

namespace PostSys.Application.Views.Forms;

/// <summary>Форма с информацией о пользователях.</summary>
public partial class UsersForm : Form
{
	public bool ButtonEditClick { get; set; }
	public List<User> DgvCurrentRow { get; set; } = null!;

	/// <summary>Создаёт экземпляр класса <see cref="UsersForm"/>.</summary>
	public UsersForm()
	{
		InitializeComponent();
	}


	#region Methods

	/// <summary>Загрузка данных в таблицу о пользователях.</summary>
	public void ShowTable()
	{
		using PostSysContext db = new();

		_dgvUsers.DataSource = db.Users.Select(x => new
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

		db.Dispose();
	}

	#endregion

	#region Handlers

	private void UsersFormLoad(object sender, EventArgs e) => ShowTable();

	private void OnAddClick(object sender, EventArgs e) => new EditUserForm(this).ShowDialog();

	private void OnEditClick(object sender, EventArgs e)
	{
		ButtonEditClick = true;

		using PostSysContext db = new();
		DgvCurrentRow = db.Users
			.Where(x => x.UserId == (int)_dgvUsers.CurrentRow.Cells[0].Value)
			.Select(x => x).ToList();

		db.Dispose();

		new EditUserForm(this).ShowDialog();
	}

	private void OnDeleteClick(object sender, EventArgs e)
	{
		if(_dgvUsers.CurrentRow != null)
		{
			using PostSysContext db = new();

			var us = db.Users
				.Where(x => x.UserId.ToString() == _dgvUsers.CurrentRow.Cells[0].Value.ToString())
				.Select(x => x).First();

			if(db.Statuses
				.Where(x => us.UserStatus == x.StatusId)
				.Select(x => x.StatusName).First() != "Admin")
			{
				db.Remove(us);
			}
			else
			{
				MessageBox.Show("Нельзя удалить администратора!");
			}
					

			db.SaveChanges();
			db.Dispose();

			ShowTable();
		}
		else
		{
			MessageBox.Show("Нет пользователей.");
		}
	}

	/// <summary>Поиск пользователя по фамилии.</summary>
	/// <param name="sender">Объект события.</param>
	/// <param name="e">Событие.</param>
	private void OnSearchUserTextChanged(object sender, EventArgs e)
	{
		using PostSysContext db = new();

		if(_txtSearchUser.Text == "")
		{
			ShowTable();
		}
		else
		{
			_dgvUsers.DataSource = db.Users.Where(x => x.UserSurname.Contains(_txtSearchUser.Text.ToLower()))
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

		db.Dispose();
	}

	private void OnSearchUserKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
			e.SuppressKeyPress = true;
	}

	#endregion

}
