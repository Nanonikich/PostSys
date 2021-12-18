using postSys.application.prj;
using postSys.application.prj.Views.Forms;
using System.Data;

namespace mUse.application.prj.Views.Forms
{
	public partial class LoginForm : Form
	{
		#region .ctor

		public LoginForm()
		{
			InitializeComponent();

			_txtPassword.UseSystemPasswordChar = true;
		}

		#endregion

		#region Events

		private void OnLoginClick(object sender, EventArgs e)
		{
			try
			{
				using PostSysContext db = new();

				IEnumerable<User> users() => from user in db.Users

											 where user.UserUsername == _txtUsername.Text && user.UserPassword == _txtPassword.Text
											 select user;

				if (users().ToList().Count != 0)
				{
					foreach (var x in from item in users().ToList()
									  let x = (from status in db.Statuses where status.StatusId == item.UserStatus select status.StatusName).First().ToString()
									  select x)
					{
						new MainForm(x).Show();
						Hide();
						break;
					}

					db.Dispose();
				}
				else
				{
					db.Dispose();
					MessageBox.Show("Неверный логин или пароль");
				}
			}
			catch
			{
				MessageBox.Show("Нет подключения к базе данных");
			}
		}

		private void LoginForm_FormClosed(object sender, FormClosedEventArgs e) => Environment.Exit(0);

		#endregion
	}
}