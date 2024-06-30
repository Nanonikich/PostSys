using Microsoft.IdentityModel.Tokens;

using Serilog;

using PostSys.Application.Context;
using PostSys.Models;

namespace PostSys.Application.Views.Forms.EditingForms;

/// <summary>Форма редактирования информации об участках.</summary>
public partial class EditUserForm : Form
{
	private static readonly ILogger Log = Serilog.Log.ForContext<EditUserForm>();

	private readonly PostSysContext _dbContext;
	private int _userId;

	/// <summary>Выбранный пользователь для редактирования.</summary>
	public User? CurrentUser { get; set; }

	/// <summary>Создаёт экземпляр класса <see cref="EditUserForm"/>.</summary>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	public EditUserForm(PostSysContext dbContext)
	{
		_dbContext = dbContext;

		InitializeComponent();
	}

	#region Methods

	/// <summary>Выбор пользователя для редактирования.</summary>
	private void SelectUserForEdit()
	{
		if(CurrentUser != null)
		{
			_btnAdd.Visible = false;

			_userId = CurrentUser.UserId;
			_txtSurname.Text = CurrentUser.UserSurname;
			_txtName.Text = CurrentUser.UserName;
			_txtPatronymic.Text = CurrentUser.UserPatronymic;
			_txtEmail.Text = CurrentUser.UserEmail;
			_txtPhone.Text = CurrentUser.UserPhone;
			_txtCity.Text = CurrentUser.UserCity;
			_txtUsername.Text = CurrentUser.UserUsername;
			_txtPassword.Text = CurrentUser.UserPassword;
		}
	}

	#endregion

	#region Handlers

	private void EditUserFormLoad(object sender, EventArgs e) => SelectUserForEdit();

	private void OnAddClick(object sender, EventArgs e)
	{
		if(!_txtSurname.Text.IsNullOrEmpty() && !_txtName.Text.IsNullOrEmpty() &&
			!_txtEmail.Text.IsNullOrEmpty() && !_txtPhone.Text.IsNullOrEmpty() && 
			!_txtCity.Text.IsNullOrEmpty() && !_txtUsername.Text.IsNullOrEmpty() && 
			!_txtPassword.Text.IsNullOrEmpty())
		{
			try
			{
				_dbContext.Users.Add(new User
				{
					UserSurname = _txtSurname.Text,
					UserName = _txtName.Text,
					UserPatronymic = _txtPatronymic.Text,
					UserEmail = _txtEmail.Text,
					UserPhone = _txtPhone.Text,
					UserCity = _txtCity.Text,
					UserUsername = _txtUsername.Text,
					UserPassword = _txtPassword.Text,
				});

				_dbContext.SaveChanges();

				MessageBox.Show("Пользователь добавлен.");
				Close();
			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				MessageBox.Show("Неверные данные.");
			}
		}
		else
		{
			MessageBox.Show("Заполните пустые поля!");
		}
	}

	private void OnEditClick(object sender, EventArgs e)
	{
		if(!_txtSurname.Text.IsNullOrEmpty() && !_txtName.Text.IsNullOrEmpty() &&
			!_txtEmail.Text.IsNullOrEmpty() && !_txtPhone.Text.IsNullOrEmpty() &&
			!_txtCity.Text.IsNullOrEmpty() && !_txtUsername.Text.IsNullOrEmpty() &&
			!_txtPassword.Text.IsNullOrEmpty())
		{
			try
			{
				var changeableUser = _dbContext.Users.FirstOrDefault(x => x.UserId == _userId);

				if(changeableUser != default)
				{
					changeableUser.UserSurname = _txtSurname.Text;
					changeableUser.UserName = _txtName.Text;
					changeableUser.UserPatronymic = _txtPatronymic.Text;
					changeableUser.UserEmail = _txtEmail.Text;
					changeableUser.UserPhone = _txtPhone.Text;
					changeableUser.UserCity = _txtCity.Text;
					changeableUser.UserUsername = _txtUsername.Text;
					changeableUser.UserPassword = _txtPassword.Text;

					_dbContext.SaveChanges();

					MessageBox.Show("Информация обновлена.");
					Close();
				}
				else
				{
					MessageBox.Show("Выбранный пользователь не найден.");
				}
			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				MessageBox.Show("Неверные данные.");
			}
		}
		else
		{
			MessageBox.Show("Заполните пустые поля!");
		}
	}

	private void TxtBoxesKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
			e.SuppressKeyPress = true;
	}

	private void OnCancelClick(object sender, EventArgs e) => Close();

	#endregion

}
