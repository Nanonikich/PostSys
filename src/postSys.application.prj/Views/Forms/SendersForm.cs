using Autofac;

using Serilog;

using PostSys.Application.Views.Controls;
using PostSys.Application.Views.Forms.EditingForms;

namespace PostSys.Application.Views.Forms;

/// <summary>Форма с отправителями.</summary>
public partial class SendersForm : Form
{
	private static readonly ILogger Log = Serilog.Log.ForContext<SendersForm>();

	private readonly DgvSendersControl _dgvSendersControl;
	private readonly ILifetimeScope _lifetimeScope;

	/// <summary>Создаёт экземпляр класса <see cref="SendersForm"/>.</summary>
	/// <param name="dgvSendersControl">Элемент управления, содержащий таблицу отправителей.</param>
	/// <param name="lifetimeScope">Хранилище сервисов.</param>
	public SendersForm(DgvSendersControl dgvSendersControl, ILifetimeScope lifetimeScope)
	{
		_dgvSendersControl = dgvSendersControl;
		_lifetimeScope = lifetimeScope;

		InitializeComponent();

		_pnlSenders.Controls.Add(_dgvSendersControl);
		_dgvSendersControl.Dock = DockStyle.Fill;
	}

	#region Handlers

	private void OnAddClick(object sender, EventArgs e) => _lifetimeScope.Resolve<EditSenderForm>().ShowDialog();

	private void OnEditClick(object sender, EventArgs e)
	{
		if(_dgvSendersControl.CurrentRow != null)
		{
			try
			{
				var currentSender = _dgvSendersControl.AllTableData.First(x => x.SenderId == (int)_dgvSendersControl.CurrentRow.Cells[0].Value);

				var editSenderForm = _lifetimeScope.Resolve<EditSenderForm>();
				editSenderForm.CurrentSender = currentSender;
				editSenderForm.ShowDialog();
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
		=> _dgvSendersControl.DeleteCurrentRow();

	/// <summary>Поиск отправителя по фамилии.</summary>
	/// <param name="sender">Объект события.</param>
	/// <param name="e">Событие.</param>
	private void OnSearchSenderTextChanged(object sender, EventArgs e)
		=> _dgvSendersControl.TextSearchChanged.Invoke(this, _txtSearchSender.Text);

	private void OnSearchSenderKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
			e.SuppressKeyPress = true;
	}

	private void OnSendersFormClosed(object sender, FormClosedEventArgs e) => _dgvSendersControl.DisposeEx();

	#endregion
}
