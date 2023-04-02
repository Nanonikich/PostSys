using System.Data;

using PostSys.Models;
using PostSys.Application.ViewModels;

namespace PostSys.Application.Views.Forms.EditingForms;

/// <summary>Форма редактирования информации об участках.</summary>
public partial class EditUserForm : Form
{
	private int _userId;
	private readonly UsersForm _userForm;

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="EditUserForm"/>.</summary>
	/// <param name="userForm">Форма с пользователями.</param>
	public EditUserForm(UsersForm userForm)
	{
		_userForm = userForm;

		InitializeComponent();
	}

	#endregion

	#region Methods

	/// <summary>Выбор пользователя для редактирования.</summary>
	private void SelectUserForEdit()
	{
		if(_userForm.ButtonEditClick)
		{
			_btnAdd.Visible = false;

			foreach(var item in _userForm.DgvCurrentRow)
			{
				_userId = item.UserId;
				_txtSurname.Text = item.UserSurname;
				_txtName.Text = item.UserName;
				_txtPatronymic.Text = item.UserPatronymic;
				_txtEmail.Text = item.UserEmail;
				_txtPhone.Text = item.UserPhone;
				_txtCity.Text = item.UserCity;
				_txtUsername.Text = item.UserUsername;
				_txtPassword.Text = item.UserPassword;
			}
		}
	}

	#endregion

	#region Handlers

	private void EditUserFormLoad(object sender, EventArgs e) => SelectUserForEdit();

	private void OnAddClick(object sender, EventArgs e)
	{
		using PostSysContext db = new();

		if( !string.IsNullOrEmpty(_txtSurname.Text) && !string.IsNullOrEmpty(_txtName.Text) &&
			!string.IsNullOrEmpty(_txtEmail.Text) && !string.IsNullOrEmpty(_txtPhone.Text) && 
			!string.IsNullOrEmpty(_txtCity.Text) && !string.IsNullOrEmpty(_txtUsername.Text) && 
			!string.IsNullOrEmpty(_txtPassword.Text))
		{
			db.Users.Add(new User
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

			db.SaveChanges();
			db.Dispose();

			_userForm.ShowTable();

			MessageBox.Show("Пользователь добавлен.");
			Close();
		}
	}

	private void OnEditClick(object sender, EventArgs e)
	{
		using PostSysContext db = new();

		if( !string.IsNullOrEmpty(_txtSurname.Text) && !string.IsNullOrEmpty(_txtName.Text) &&
			!string.IsNullOrEmpty(_txtEmail.Text) && !string.IsNullOrEmpty(_txtPhone.Text) &&
			!string.IsNullOrEmpty(_txtCity.Text) && !string.IsNullOrEmpty(_txtUsername.Text) &&
			!string.IsNullOrEmpty(_txtPassword.Text))
		{
			foreach(var item in db.Users.Where(x => x.UserId.Equals(_userId)).Select(x => x).ToList())
			{
				item.UserSurname = _txtSurname.Text;
				item.UserName = _txtName.Text;
				item.UserPatronymic = _txtPatronymic.Text;
				item.UserEmail = _txtEmail.Text;
				item.UserPhone = _txtPhone.Text;
				item.UserCity = _txtCity.Text;
				item.UserUsername = _txtUsername.Text;
				item.UserPassword = _txtPassword.Text;
			}
			db.SaveChanges();
			db.Dispose();

			_userForm.ShowTable();

			MessageBox.Show("Информация обновлена.");
			Close();
		}
		else
		{
			db.Dispose();
			MessageBox.Show("Заполните пустые поля!");
		}
	}

	private void TxtBoxesKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
		{
			e.SuppressKeyPress = true;
		}
	}

	private void OnCancelClick(object sender, EventArgs e) => Close();

	private void OnFormClosed(object sender, FormClosedEventArgs e) => _userForm.ButtonEditClick = false;

	#endregion

}
