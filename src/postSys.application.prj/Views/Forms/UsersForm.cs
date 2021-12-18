using postSys.application.prj.Views.Forms.EditingForms;
using System.Data;

namespace postSys.application.prj.Views.Forms
{
	public partial class UsersForm : Form
	{
		#region Properties

		public bool ButtonEditClick { get; set; }
		public List<User> DgvCurrentRow { get; set; } = null!;

		#endregion

		#region .ctor

		public UsersForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		public void ShowTable()
		{
			using PostSysContext db = new();

			_dgvUsers.DataSource = (from user in db.Users
									   select new
									   {
										   ID = user.UserId,
										   Фамилия = user.UserSurname,
										   Имя = user.UserName,
										   Отчество = user.UserPatronymic,
										   Email = user.UserEmail,
										   Телефон = user.UserPhone,
										   Участок = user.UserCity,
										   Логин = user.UserUsername,
										   Пароль = user.UserPassword,
									   }).ToList();
			db.Dispose();
		}

		#endregion

		#region Events

		private void UsersForm_Load(object sender, EventArgs e) => ShowTable();

		private void OnAddClick(object sender, EventArgs e) => new EditUserForm(this).ShowDialog();

		private void OnEditClick(object sender, EventArgs e)
		{
			ButtonEditClick = true;

			using PostSysContext db = new();
			DgvCurrentRow = (from user in db.Users
								where user.UserId.ToString() == _dgvUsers.CurrentRow.Cells[0].Value.ToString()
								select user).ToList();
			db.Dispose();

			new EditUserForm(this).ShowDialog();
		}

		private void OnDeleteClick(object sender, EventArgs e)
		{
			if (_dgvUsers.CurrentRow != null)
			{
				using PostSysContext db = new();

				var us = ((from user in db.Users
							where user.UserId.ToString() == _dgvUsers.CurrentRow.Cells[0].Value.ToString()
							select user).First());

				if ((from status in db.Statuses 
						where us.UserStatus == status.StatusId select status.StatusName).First().ToString() != "Admin")
				{
					db.Remove(us);
				}
				else
				{
					MessageBox.Show("Нельзя удалить администратора");
				}
						

				db.SaveChanges();
				db.Dispose();

				ShowTable();
			}
			else
			{
				MessageBox.Show("Нет пользователей");
			}
		}

		private void OnSearchUserTextChanged(object sender, EventArgs e)
		{
			using PostSysContext db = new();

			if (_txtSearchUser.Text == "")
			{
				ShowTable();
			}
			else
			{
				_dgvUsers.DataSource = (from user in db.Users
										where user.UserSurname.Contains(_txtSearchUser.Text.ToLower())
										select new
										{
											ID = user.UserId,
											Фамилия = user.UserSurname,
											Имя = user.UserName,
											Отчество = user.UserPatronymic,
											Email = user.UserEmail,
											Телефон = user.UserPhone,
											Участок = user.UserCity,
											Логин = user.UserUsername,
											Пароль = user.UserPassword,
										}).ToList();
			}

			db.Dispose();
		}

		private void OnSearchUserKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				e.SuppressKeyPress = true;
		}

		#endregion

	}
}
