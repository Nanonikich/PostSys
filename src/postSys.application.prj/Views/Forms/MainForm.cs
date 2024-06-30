using Autofac;

using Serilog;

using PostSys.Application.Helpers;
using PostSys.Application.Views.Controls;
using PostSys.Application.Views.Forms.EditingForms;

namespace PostSys.Application.Views.Forms;

/// <summary>Главная форма.</summary>
public partial class MainForm : Form
{
	private static readonly ILogger Log = Serilog.Log.ForContext<MainForm>();

	private readonly DgvAddressesControl _dgvAddressesControl;

	private readonly UserInfo _userInfo;
	private readonly ILifetimeScope _lifetimeScope;

	/// <summary>Создаёт экземпляр класса <see cref="MainForm"/>.</summary>
	/// <param name="dgvAddressesControl">Элемент управления, содержащий таблицу адресов.</param>
	/// <param name="lifetimeScope">Хранилище сервисов.</param>
	public MainForm(DgvAddressesControl dgvAddressesControl, ILifetimeScope lifetimeScope)
	{
		_dgvAddressesControl = dgvAddressesControl;

		_userInfo = lifetimeScope.Resolve<UserInfo>();
		_lifetimeScope = lifetimeScope;

		InitializeComponent();

		_pnlAddresses.Controls.Add(_dgvAddressesControl);
		_dgvAddressesControl.Dock = DockStyle.Fill;
	}

	#region Handlers

	private void MainFormLoad(object sender, EventArgs e)
	{
		if(_userInfo.UserRole != "Администратор")
			_btnUsers.Visible = false;
	}

	private void OnAddClick(object sender, EventArgs e) => _lifetimeScope.Resolve<EditAddressForm>().ShowDialog();

	private void OnEditClick(object sender, EventArgs e)
	{
		if(_dgvAddressesControl.CurrentRow != null)
		{
			try
			{
				var currentAddress = _dgvAddressesControl.AllTableData.First(x => x.AddressId == (int)_dgvAddressesControl.CurrentRow.Cells[0].Value);

				var editAddressForm = _lifetimeScope.Resolve<EditAddressForm>();
				editAddressForm.CurrentAddress = currentAddress;
				editAddressForm.ShowDialog();
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
		=> _dgvAddressesControl.DeleteCurrentRow();

	/// <summary>Поиск отправителя или почтальона.</summary>
	/// <param name="sender">Объект события.</param>
	/// <param name="e">Событие.</param>
	private void OnSearchRecipientOrPostmanTextChanged(object sender, EventArgs e)
		=> _dgvAddressesControl.TextSearchChanged.Invoke(this, (_txtSearchRecipient.Text, _txtSearchPostman.Text));

	private void OnTxtBoxesKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
			e.SuppressKeyPress = true;
	}

	private void OnPostmansClick(object sender, EventArgs e) => _lifetimeScope.Resolve<PostmansForm>().ShowDialog();

	private void OnSendersClick(object sender, EventArgs e) => _lifetimeScope.Resolve<SendersForm>().ShowDialog();

	private void OnClientsClick(object sender, EventArgs e) => _lifetimeScope.Resolve<RecipientsForm>().ShowDialog();

	private void OnCitiesClick(object sender, EventArgs e) => _lifetimeScope.Resolve<StreetCityForm>().ShowDialog();

	private void OnPlotsClick(object sender, EventArgs e) => _lifetimeScope.Resolve<PlotsForm>().ShowDialog();

	private void OnUsersClick(object sender, EventArgs e) => _lifetimeScope.Resolve<UsersForm>().ShowDialog();

	private void MainFormClosed(object sender, FormClosedEventArgs e)
		=> _dgvAddressesControl.DisposeEx();

	#endregion

}