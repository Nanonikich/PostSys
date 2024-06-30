using Autofac;

using Serilog;

using PostSys.Application.Views.Controls;
using PostSys.Application.Views.Forms.EditingForms;

namespace PostSys.Application.Views.Forms;

/// <summary>Форма с информацией о почтальонах.</summary>
public partial class PostmansForm : Form
{
	private static readonly ILogger Log = Serilog.Log.ForContext<PostmansForm>();

	private readonly DgvPostmansControl _dgvPostmansControl;
	private readonly ILifetimeScope _lifetimeScope;

	/// <summary>Создаёт экземпляр класса <see cref="PostmansForm"/>.</summary>
	/// <param name="dgvPostmansControl">Элемент управления, содержащий таблицу почтальонов.</param>
	/// <param name="lifetimeScope">Хранилище сервисов.</param>
	public PostmansForm(DgvPostmansControl dgvPostmansControl, ILifetimeScope lifetimeScope)
	{
		_dgvPostmansControl = dgvPostmansControl;
		_lifetimeScope = lifetimeScope;

		InitializeComponent();

		_pnlPostmans.Controls.Add(_dgvPostmansControl);
		_dgvPostmansControl.Dock = DockStyle.Fill;
	}

	#region Handlers

	private void OnAddClick(object sender, EventArgs e) => _lifetimeScope.Resolve<EditPostmanForm>().ShowDialog();

	private void OnEditClick(object sender, EventArgs e)
	{
		if(_dgvPostmansControl.CurrentRow != null)
		{
			try
			{
				var currentPostman = _dgvPostmansControl.AllTableData.First(x => x.PostmanId == (int)_dgvPostmansControl.CurrentRow.Cells[0].Value);

				var editPostmanForm = _lifetimeScope.Resolve<EditPostmanForm>();
				editPostmanForm.CurrentPostman = currentPostman;
				editPostmanForm.ShowDialog();
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
		=> _dgvPostmansControl.DeleteCurrentRow();

	/// <summary>Поиск почтальона по участку.</summary>
	/// <param name="sender">Объект события.</param>
	/// <param name="e">Событие.</param>
	private void OnSearchPlotTextChanged(object sender, EventArgs e)
		=> _dgvPostmansControl.TextSearchChanged.Invoke(this, _txtSearchPlot.Text);

	private void OnSearchPlotKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
			e.SuppressKeyPress = true;
	}

	private void PostmansFormClosed(object sender, FormClosedEventArgs e) => _dgvPostmansControl.DisposeEx();

	#endregion

}
