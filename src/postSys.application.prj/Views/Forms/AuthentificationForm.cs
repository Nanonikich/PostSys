using Serilog;

using PostSys.Application.Helpers;
using PostSys.Application.Context;

namespace PostSys.Application.Views.Forms;

/// <summary>Форма аутентификации.</summary>
public partial class AuthentificationForm : Form
{
	private static readonly ILogger Log = Serilog.Log.ForContext<AuthentificationForm>();

	private readonly PostSysContext _dbContext;
	private readonly MainForm _mainForm;

	private UserInfo? _userInfo;

	/// <summary>Создаёт экземпляр класса <see cref="AuthentificationForm"/>.</summary>
	/// <param name="userInfo">Информация о пользователе.</param>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	/// <param name="mainForm">Основная форма.</param>
	public AuthentificationForm(UserInfo userInfo, PostSysContext dbContext, MainForm mainForm)
	{
		_userInfo = userInfo;
		_dbContext = dbContext;
		_mainForm = mainForm;

		_mainForm.FormClosed += LoginFormClosed;

		InitializeComponent();
	}

	#region Handlers

	private void OnLoginClick(object sender, EventArgs e)
	{
		try
		{
			var user = _dbContext.Users.FirstOrDefault(x => x.UserUsername == _txtUsername.Text &&
															x.UserPassword == _txtPassword.Text);

			if(user != default)
			{
				_userInfo!.UserId = user.UserId;
				_userInfo.UserUsername = user.UserName;
				_userInfo.UserRole = _dbContext.Role.First(x => user.UserRole == x.RoleId).RoleName;

				Log.Information($"Пользователь '{user.UserId}: {user.UserUsername}' выполнил вход.");

				_mainForm.ShowDialog();
				_mainForm.FormClosed += LoginFormClosed;

				Hide();
			}
			else
			{
				MessageBox.Show("Неверный логин или пароль.");
			}
		}
		catch(Exception ex)
		{
			Log.Error(ex.Message);
			MessageBox.Show("Нет подключения к базе данных.");
		}
	}

	private void TxtBoxesKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
			e.SuppressKeyPress = true;
	}

	private void LoginFormClosed(object? sender, FormClosedEventArgs e)
	{
		_mainForm.FormClosed -= LoginFormClosed;

		Log.Information($"Пользователь {_userInfo?.UserId}:{_userInfo?.UserUsername} вышел из системы.");
		System.Windows.Forms.Application.Exit();
	}

	#endregion

}