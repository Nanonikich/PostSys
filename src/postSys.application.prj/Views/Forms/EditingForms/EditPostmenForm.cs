using System.Data;

using PostSys.Models;
using PostSys.Application.ViewModels;

namespace PostSys.Application.Views.Forms.EditingForms;

/// <summary>Форма редактирования почтальонов.</summary>
public partial class EditPostmenForm : Form
{
	private int _postmenId;
	private readonly PostmensForm _postmenForm;

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="EditPostmenForm"/>.</summary>
	/// <param name="postmenForm">Форма с почтальонами.</param>
	public EditPostmenForm(PostmensForm postmenForm)
	{
		_postmenForm = postmenForm;

		InitializeComponent();
	}

	#endregion

	#region Methods

	/// <summary>Загрузка участков в ComboBox.</summary>
	private void SettingCbxPlot()
	{
		using PostSysContext db = new();

		_cbxPlot.DataSource = db.CodesAddresses.Select(x => x).ToList();
		_cbxPlot.ValueMember = "CodeAddressId";
		_cbxPlot.DisplayMember = "CodeAddressPlot";

		db.Dispose();
	}

	/// <summary>Выбор почтальонов для редактирования.</summary>
	private void SelectPostmenForEdit()
	{
		if(_postmenForm.ButtonEditClick)
		{
			_btnAdd.Visible = false;

			foreach(var item in _postmenForm.DgvCurrentRow)
			{
				_postmenId = item.PostmenId;
				_txtSurname.Text = item.PostmenSurname;
				_txtName.Text = item.PostmenName;
				_txtPatronymic.Text = item.PostmenPatronymic;
				_txtPhone.Text = item.PostmenPhone;
				_cbxPlot.Text = item.PostmenPlot.ToString();
			}
		}
	}

	#endregion

	#region Handlers

	private void EditPostmenFormLoad(object sender, EventArgs e)
	{
		SettingCbxPlot();

		SelectPostmenForEdit();
	}

	private void OnAddClick(object sender, EventArgs e)
	{
		using PostSysContext db = new();

		if(!string.IsNullOrEmpty(_txtSurname.Text) && !string.IsNullOrEmpty(_txtName.Text) &&
			!string.IsNullOrEmpty(_txtPhone.Text) && !string.IsNullOrEmpty(_cbxPlot.Text))
		{
			db.Postmens.Add(new Postmen
			{
				PostmenSurname = _txtSurname.Text,
				PostmenName = _txtName.Text,
				PostmenPatronymic = _txtPatronymic.Text,
				PostmenPhone = _txtPhone.Text,
				PostmenPlot = int.Parse(_cbxPlot.Text)
			});

			db.SaveChanges();
			db.Dispose();

			_postmenForm.ShowTable();

			MessageBox.Show("Почтальон добавлен.");
			Close();
		}
	}

	private void OnEditClick(object sender, EventArgs e)
	{
		using PostSysContext db = new();

		if(!string.IsNullOrEmpty(_txtSurname.Text) && !string.IsNullOrEmpty(_txtName.Text) &&
			!string.IsNullOrEmpty(_txtPhone.Text) && !string.IsNullOrEmpty(_cbxPlot.Text))
		{
			foreach(var item in db.Postmens
				.Where(x => x.PostmenId.Equals(_postmenId))
				.Select(x => x).ToList())
			{
				item.PostmenSurname = _txtSurname.Text;
				item.PostmenName = _txtName.Text;
				item.PostmenPatronymic = _txtPatronymic.Text;
				item.PostmenPhone = _txtPhone.Text;
				item.PostmenPlot = int.Parse(_cbxPlot.Text);
			}
			db.SaveChanges();
			db.Dispose();

			_postmenForm.ShowTable();
			_postmenForm.ComboBoxUpd();

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

	private void OnFormClosed(object sender, FormClosedEventArgs e) => _postmenForm.ButtonEditClick = false;

	#endregion
}
