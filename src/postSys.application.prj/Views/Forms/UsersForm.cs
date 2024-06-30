using Autofac;

using Serilog;

using PostSys.Application.Views.Controls;
using PostSys.Application.Views.Forms.EditingForms;

namespace PostSys.Application.Views.Forms;

/// <summary>Форма с информацией о пользователях.</summary>
public partial class UsersForm : Form
{
	private static readonly ILogger Log = Serilog.Log.ForContext<UsersForm>();

	private readonly DgvUsersControl _dgvUsersControl;
	private readonly ILifetimeScope _lifetimeScope;

	/// <summary>Создаёт экземпляр класса <see cref="UsersForm"/>.</summary>
	/// <param name="dgvUsersControl">Элемент управления, содержащий таблицу с информацией о пользователях.</param>
	/// <param name="lifetimeScope">Хранилище сервисов.</param>
	public UsersForm(DgvUsersControl dgvUsersControl, ILifetimeScope lifetimeScope)
	{
		_dgvUsersControl = dgvUsersControl;
		_lifetimeScope = lifetimeScope;

		InitializeComponent();

		_pnlUsers.Controls.Add(_dgvUsersControl);
		_dgvUsersControl.Dock = DockStyle.Fill;
	}

	#region Handlers

	private void OnAddClick(object sender, EventArgs e) => _lifetimeScope.Resolve<EditUserForm>().ShowDialog();

	private void OnEditClick(object sender, EventArgs e)
	{
		if(_dgvUsersControl.CurrentRow != null)
		{
			try
			{
				var currentUser = _dgvUsersControl.AllTableData.First(x => x.UserId == (int)_dgvUsersControl.CurrentRow.Cells[0].Value);

				var editUserForm = _lifetimeScope.Resolve<EditUserForm>();
				editUserForm.CurrentUser = currentUser;
				editUserForm.ShowDialog();
			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				MessageBox.Show($"Произошла ошибка. Не удалось установить данные для редактирования.");
			}
		}
		else
		{
			MessageBox.Show($"Не выбраны данные для редактирования.");
		}
	}

	private void OnDeleteClick(object sender, EventArgs e)
		=> _dgvUsersControl.DeleteCurrentRow();

	/// <summary>Поиск пользователя по фамилии.</summary>
	/// <param name="sender">Объект события.</param>
	/// <param name="e">Событие.</param>
	private void OnSearchUserTextChanged(object sender, EventArgs e)
		=> _dgvUsersControl.TextSearchChanged.Invoke(this, _txtSearchUser.Text);

	private void OnSearchUserKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
			e.SuppressKeyPress = true;
	}

	private void OnUserFormClosed(object sender, FormClosedEventArgs e) => _dgvUsersControl.DisposeEx();

	#endregion

}
