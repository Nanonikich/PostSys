using Microsoft.Data.SqlClient;

using PostSys.Application.ViewModels;

namespace PostSys.Application.Views.Forms;

/// <summary>Форма авторизации.</summary>
public partial class LoginForm : Form
{
	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="LoginForm"/>.</summary>
	public LoginForm()
	{
		InitializeComponent();

		_txtPassword.UseSystemPasswordChar = true;
	}

	#endregion

	#region Handlers

	private void OnLoginClick(object sender, EventArgs e)
	{
		try
		{
			using PostSysContext db = new();

			var user = db.Users.FirstOrDefault(x => x.UserUsername == _txtUsername.Text &&
													x.UserPassword == _txtPassword.Text);

			if(user != default)
			{
				new MainForm(user.UserId, db.Statuses.First(x => user.UserStatus == x.StatusId).StatusName).Show();
				Hide();

				db.Dispose();
			}
			else
			{
				db.Dispose();
				MessageBox.Show("Неверный логин или пароль.");
			}
		}
		catch(SqlException)
		{
			MessageBox.Show("Нет подключения к базе данных.");
		}
	}

	private void TxtBoxesKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
		{
			e.SuppressKeyPress = true;
		}
	}

	private void LoginFormClosed(object sender, FormClosedEventArgs e)
		=> System.Windows.Forms.Application.Exit();

	#endregion

}