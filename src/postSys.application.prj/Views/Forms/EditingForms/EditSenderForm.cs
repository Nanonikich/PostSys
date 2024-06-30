using Microsoft.IdentityModel.Tokens;

using Serilog;

using PostSys.Application.Context;
using PostSys.Models;

namespace PostSys.Application.Views.Forms.EditingForms;

/// <summary>Форма редактирования отправителей.</summary>
public partial class EditSenderForm : Form
{
	private static readonly ILogger Log = Serilog.Log.ForContext<EditSenderForm>();

	private readonly PostSysContext _dbContext;
	private int _senderId;

	/// <summary>Выбранный отправитель для редактирования.</summary>
	public Sender? CurrentSender { get; set; }

	/// <summary>Создаёт экземпляр класса <see cref="EditSenderForm"/>.</summary>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	public EditSenderForm(PostSysContext dbContext)
	{
		_dbContext = dbContext;

		InitializeComponent();
	}

	#region Methods

	/// <summary>Загрузка городов в ComboBox.</summary>
	private void SettingCbxCity()
	{
		_cbxCity.DataSource = _dbContext.City.ToList();
		_cbxCity.ValueMember = "CityId";
		_cbxCity.DisplayMember = "CityName";
	}

	/// <summary>Загрузка улиц в ComboBox.</summary>
	private void SettingCbxStreet()
	{
		if(_cbxCity.SelectedValue != null)
		{
			_cbxStreet.DataSource = _dbContext.AddressCode
				.Where(x => x.AddressCodeCity.ToString() == _cbxCity.SelectedValue.ToString())
				.Select(x => new
				{
					x.AddressCodeId,
					x.AddressCodeCityNavigation.CityName,
					x.AddressCodePlot,
					x.AddressCodeStreetNavigation.StreetName,
					x.AddressCodeHouses,
				}).ToList();

			_cbxStreet.ValueMember = "AddressCodeId";
			_cbxStreet.DisplayMember = "StreetName";
		}
	}

	/// <summary>Загрузка домов в ComboBox.</summary>
	private void SettingCbxHome()
	{
		if(_cbxCity.SelectedValue != null && !_cbxStreet.Text.IsNullOrEmpty())
		{
			_cbxHome.DataSource = _dbContext.AddressCode
				.Where(x => x.AddressCodeCity.ToString() == _cbxCity.SelectedValue.ToString() &&
					x.AddressCodeStreetNavigation.StreetName == _cbxStreet.Text
					&& x.AddressCodeHouses != null)
				.Select(x => x.AddressCodeHouses)
				.FirstOrDefault()?.Split(", ");
		}
	}

	/// <summary>Выбор отправителя для редактирования.</summary>
	private void SelectSenderForEdit()
	{
		if(CurrentSender != null)
		{
			_btnAdd.Visible = false;

			_senderId = CurrentSender.SenderId;
			_txtSurname.Text = CurrentSender.SenderSurname;
			_txtName.Text = CurrentSender.SenderName;
			_txtPatronymic.Text = CurrentSender.SenderPatronymic;
			_cbxCity.SelectedValue = CurrentSender.SenderCity;
			_cbxStreet.SelectedValue = CurrentSender.SenderStreet;
			_cbxHome.Text = CurrentSender.SenderHome;
			_txtApartment.Text = CurrentSender.SenderApartment.ToString();
			_txtPhone.Text = CurrentSender.SenderPhone;
		}
	}

	#endregion

	#region Handlers

	private void EditSenderFormLoad(object sender, EventArgs e)
	{
		SettingCbxCity();
		SettingCbxStreet();
		SettingCbxHome();

		SelectSenderForEdit();
	}

	private void OnAddClick(object sender, EventArgs e)
	{
		if(!_txtSurname.Text.IsNullOrEmpty() && !_txtName.Text.IsNullOrEmpty() &&
			_cbxCity.SelectedValue != null && _cbxStreet.SelectedValue != null && 
			!_cbxHome.Text.IsNullOrEmpty() && !_txtApartment.Text.IsNullOrEmpty() && 
			!_txtPhone.Text.IsNullOrEmpty())
		{
			try
			{
				_dbContext.Sender.Add(new Sender
				{
					SenderSurname = _txtSurname.Text,
					SenderName = _txtName.Text,
					SenderPatronymic = _txtPatronymic.Text,
					SenderCity = (int)_cbxCity.SelectedValue,
					SenderStreet = (int)_cbxStreet.SelectedValue,
					SenderHome = _cbxHome.Text,
					SenderApartment = int.Parse(_txtApartment.Text),
					SenderPhone = _txtPhone.Text,
				});
				_dbContext.SaveChanges();

				MessageBox.Show("Отправитель добавлен.");
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
			MessageBox.Show("Заполните пустые поля.");
		}
	}

	private void OnEditClick(object sender, EventArgs e)
	{
		if(!_txtSurname.Text.IsNullOrEmpty() && !_txtName.Text.IsNullOrEmpty() &&
			_cbxCity.SelectedValue != null && _cbxStreet.SelectedValue != null && 
			!_cbxHome.Text.IsNullOrEmpty() && !_txtApartment.Text.IsNullOrEmpty() && 
			!_txtPhone.Text.IsNullOrEmpty())
		{
			try
			{
				var changeableSender = _dbContext.Sender.FirstOrDefault(x => x.SenderId == _senderId);

				if(changeableSender != default)
				{
					changeableSender.SenderSurname = _txtSurname.Text;
					changeableSender.SenderName = _txtName.Text;
					changeableSender.SenderPatronymic = _txtPatronymic.Text;
					changeableSender.SenderCity = (int)_cbxCity.SelectedValue;
					changeableSender.SenderStreet = (int)_cbxStreet.SelectedValue;
					changeableSender.SenderHome = _cbxHome.Text;
					changeableSender.SenderApartment = int.Parse(_txtApartment.Text);
					changeableSender.SenderPhone = _txtPhone.Text;

					_dbContext.SaveChanges();

					MessageBox.Show("Информация обновлена.");
					Close();
				}
				else
				{
					MessageBox.Show("Выбранный отправитель не найден.");
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
			MessageBox.Show("Заполните пустые поля.");
		}
	}

	private void TxtBoxesKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
			e.SuppressKeyPress = true;
	}

	private void OnCbxCitySelectedIndexChanged(object sender, EventArgs e) => SettingCbxStreet();

	private void OnCbxStreetSelectedIndexChanged(object sender, EventArgs e) => SettingCbxHome();

	private void OnCancelClick(object sender, EventArgs e) => Close();

	#endregion
}