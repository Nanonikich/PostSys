using Autofac;

using Serilog;

using PostSys.Application.Views.Controls;
using PostSys.Application.Views.Forms.EditingForms;

namespace PostSys.Application.Views.Forms;

/// <summary>Форма с информацией о получателях.</summary>
public partial class RecipientsForm : Form
{
	private static readonly ILogger Log = Serilog.Log.ForContext<RecipientsForm>();

	private readonly DgvRecipientsControl _dgvRecipientsControl;
	private readonly ILifetimeScope _lifetimeScope;

	/// <summary>Создаёт экземпляр класса <see cref="RecipientsForm"/>.</summary>
	/// <param name="dgvRecipientsControl">Элемент управления, содержащий таблицу получателей.</param>
	/// <param name="lifetimeScope">Хранилище сервисов.</param>
	public RecipientsForm(DgvRecipientsControl dgvRecipientsControl, ILifetimeScope lifetimeScope)
	{
		_dgvRecipientsControl = dgvRecipientsControl;
		_lifetimeScope = lifetimeScope;

		InitializeComponent();

		_pnlRecipients.Controls.Add(_dgvRecipientsControl);
		_dgvRecipientsControl.Dock = DockStyle.Fill;
	}

	#region Handlers

	private void OnAddClick(object sender, EventArgs e) => _lifetimeScope.Resolve<EditRecipientForm>().ShowDialog();

	private void OnEditClick(object sender, EventArgs e)
	{
		if(_dgvRecipientsControl.CurrentRow != null)
		{
			try
			{
				var currentRecipient = _dgvRecipientsControl.AllTableData.First(x => x.RecipientId == (int)_dgvRecipientsControl.CurrentRow.Cells[0].Value);

				var editRecipientForm = _lifetimeScope.Resolve<EditRecipientForm>();
				editRecipientForm.CurrentRecipient = currentRecipient;
				editRecipientForm.ShowDialog();
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
		=> _dgvRecipientsControl.DeleteCurrentRow();

	/// <summary>Поиск получателя по фамилии.</summary>
	/// <param name="sender">Объект события.</param>
	/// <param name="e">Событие.</param>
	private void OnSearchSenderOrRecipientTextChanged(object sender, EventArgs e)
		=> _dgvRecipientsControl.TextSearchChanged.Invoke(this, (_txtSearchSender.Text, _txtSearchRecipient.Text));

	private void OnTxtBoxesKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
			e.SuppressKeyPress = true;
	}

	private void OnRecipientsFormClosed(object sender, FormClosedEventArgs e) => _dgvRecipientsControl.DisposeEx();


	#endregion
}