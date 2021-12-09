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
			using PostSysContext db = new();

			IEnumerable<User> users() => from user in db.Users
										 where user.UserUsername == _txtUsername.Text && user.UserPassword == _txtPassword.Text.ToString()
										 select user;

			if (users().ToList().Count != 0)
			{
				db.Dispose();
				new MainForm().Show();
				Hide();
			}
			else
			{
				db.Dispose();
				MessageBox.Show("Неверный логин или пароль");
			}
		}

		private void LoginForm_FormClosed(object sender, FormClosedEventArgs e) => Environment.Exit(0);

		#endregion
	}
}