using Microsoft.IdentityModel.Tokens;

using Serilog;

using PostSys.Application.Context;
using PostSys.Models;

namespace PostSys.Application.Views.Forms.EditingForms;

/// <summary>Форма редактирования почтальонов.</summary>
public partial class EditPostmanForm : Form
{
	private static readonly ILogger Log = Serilog.Log.ForContext<EditPostmanForm>();

	private readonly PostSysContext _dbContext;
	private int _postmanId;

	/// <summary>Выбранный почтальон для редактирования.</summary>
	public Postman? CurrentPostman { get; set; }

	/// <summary>Создаёт экземпляр класса <see cref="EditPostmanForm"/>.</summary>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	public EditPostmanForm(PostSysContext dbContext)
	{
		_dbContext = dbContext;

		InitializeComponent();
	}

	#region Methods

	/// <summary>Загрузка участков в ComboBox.</summary>
	private void SettingCbxPlot()
	{
		_cbxPlot.DataSource = _dbContext.AddressCode.ToList();
		_cbxPlot.ValueMember = "AddressCodeId";
		_cbxPlot.DisplayMember = "AddressCodePlot";
	}

	/// <summary>Выбор почтальонов для редактирования.</summary>
	private void SelectPostmanForEdit()
	{
		if(CurrentPostman != null)
		{
			_btnAdd.Visible = false;

			_postmanId = CurrentPostman.PostmanId;
			_txtSurname.Text = CurrentPostman.PostmanSurname;
			_txtName.Text = CurrentPostman.PostmanName;
			_txtPatronymic.Text = CurrentPostman.PostmanPatronymic;
			_txtPhone.Text = CurrentPostman.PostmanPhone;
			_cbxPlot.Text = CurrentPostman.PostmanPlot.ToString();
		}
	}

	#endregion

	#region Handlers

	private void EditPostmanFormLoad(object sender, EventArgs e)
	{
		SettingCbxPlot();
		SelectPostmanForEdit();
	}

	private void OnAddClick(object sender, EventArgs e)
	{
		if(!_txtSurname.Text.IsNullOrEmpty() && !_txtName.Text.IsNullOrEmpty() &&
			!_txtPhone.Text.IsNullOrEmpty() && !_cbxPlot.Text.IsNullOrEmpty())
		{
			try
			{
				_dbContext.Postman.Add(new Postman
				{
					PostmanSurname = _txtSurname.Text,
					PostmanName = _txtName.Text,
					PostmanPatronymic = _txtPatronymic.Text,
					PostmanPhone = _txtPhone.Text,
					PostmanPlot = int.Parse(_cbxPlot.Text)
				});

				_dbContext.SaveChanges();

				MessageBox.Show("Почтальон добавлен.");
				Close();
			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				MessageBox.Show("Неверные данные.");
			}
		}
	}

	private void OnEditClick(object sender, EventArgs e)
	{
		if(!_txtSurname.Text.IsNullOrEmpty() && !_txtName.Text.IsNullOrEmpty() &&
			!_txtPhone.Text.IsNullOrEmpty() && !_cbxPlot.Text.IsNullOrEmpty())
		{
			try
			{
				var changeablePostman = _dbContext.Postman.FirstOrDefault(x => x.PostmanId == _postmanId);

				if(changeablePostman != default)
				{
					changeablePostman.PostmanSurname = _txtSurname.Text;
					changeablePostman.PostmanName = _txtName.Text;
					changeablePostman.PostmanPatronymic = _txtPatronymic.Text;
					changeablePostman.PostmanPhone = _txtPhone.Text;
					changeablePostman.PostmanPlot = int.Parse(_cbxPlot.Text);

					_dbContext.SaveChanges();

					MessageBox.Show("Информация обновлена.");
					Close();
				}
				else
				{
					MessageBox.Show("Выбранный почтальон не найден.");
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
