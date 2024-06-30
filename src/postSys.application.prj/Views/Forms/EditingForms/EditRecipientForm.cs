using Microsoft.IdentityModel.Tokens;

using Serilog;

using PostSys.Application.Context;
using PostSys.Models;

namespace PostSys.Application.Views.Forms.EditingForms;

/// <summary>Форма редактирования информации о получателях.</summary>
public partial class EditRecipientForm : Form
{
	private static readonly ILogger Log = Serilog.Log.ForContext<EditRecipientForm>();

	private readonly PostSysContext _dbContext;
	private int _recipientId;

	/// <summary>Выбранный получатель для редактирования.</summary>
	public Recipient? CurrentRecipient { get; set; }

	/// <summary>Создаёт экземпляр класса <see cref="EditRecipientForm"/>.</summary>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	public EditRecipientForm(PostSysContext dbContext)
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
					x.AddressCodeHouses
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

	/// <summary>Загрузка отправителей в ComboBox.</summary>
	private void SettingCbxSender()
	{
		_cbxSenders.DataSource = _dbContext.Sender.ToList();
		_cbxSenders.ValueMember = "SenderId";
		_cbxSenders.DisplayMember = "SenderSurname";

		if(_cbxSenders.SelectedValue != null)
			_lblSender.Text = _cbxSenders.SelectedValue.ToString();
	}

	/// <summary>Выбор получателя для редактирования.</summary>
	private void SelectRecipientForEdit()
	{
		if(CurrentRecipient != null)
		{
			_btnAdd.Visible = false;

			_recipientId = CurrentRecipient.RecipientId;
			_txtSeries.Text = CurrentRecipient.RecipientSeries?.ToString();
			_txtNumber.Text = CurrentRecipient.RecipientNumber?.ToString();
			_txtSurname.Text = CurrentRecipient.RecipientSurname;
			_txtName.Text = CurrentRecipient.RecipientName;
			_txtPatronymic.Text = CurrentRecipient.RecipientPatronymic;
			_cbxCity.SelectedValue = CurrentRecipient.RecipientCity;
			_cbxStreet.SelectedValue = CurrentRecipient.RecipientStreet;
			_cbxHome.Text = CurrentRecipient.RecipientHome;
			_txtApartments.Text = CurrentRecipient.RecipientApartment.ToString();
			_txtPhone.Text = CurrentRecipient.RecipientPhone;
			_cbxSenders.SelectedValue = CurrentRecipient.RecipientSender;
		}
	}

	#endregion

	#region Handlers

	private void EditClientFormLoad(object sender, EventArgs e)
	{
		SettingCbxCity();
		SettingCbxStreet();
		SettingCbxHome();
		SettingCbxSender();

		SelectRecipientForEdit();
	}

	private void OnAddClick(object sender, EventArgs e)
	{		
		if( !_txtSurname.Text.IsNullOrEmpty() && !_txtName.Text.IsNullOrEmpty() &&
		    _cbxCity.SelectedValue != null && _cbxStreet.SelectedValue != null && 
			!_cbxHome.Text.IsNullOrEmpty() && !_txtApartments.Text.IsNullOrEmpty() && 
			!_txtPhone.Text.IsNullOrEmpty() && _cbxSenders.SelectedValue != null)
		{
			try
			{
				_dbContext.Recipient.Add(new Recipient
				{
					RecipientSeries = _txtSeries.Text,
					RecipientNumber = _txtNumber.Text,
					RecipientSurname = _txtSurname.Text,
					RecipientName = _txtName.Text,
					RecipientPatronymic = _txtPatronymic.Text,
					RecipientCity = (int)_cbxCity.SelectedValue,
					RecipientStreet = (int)_cbxStreet.SelectedValue,
					RecipientHome = _cbxHome.Text,
					RecipientApartment = int.Parse(_txtApartments.Text),
					RecipientPhone = _txtPhone.Text,
					RecipientSender = (int)_cbxSenders.SelectedValue
				});
				_dbContext.SaveChanges();

				MessageBox.Show("Получатель добавлен.");
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
		if( !_txtSurname.Text.IsNullOrEmpty() && !_txtName.Text.IsNullOrEmpty() &&
			_cbxCity.SelectedValue != null && _cbxStreet.SelectedValue != null && 
			!_cbxHome.Text.IsNullOrEmpty() && !_txtApartments.Text.IsNullOrEmpty() && 
			!_txtPhone.Text.IsNullOrEmpty() && _cbxSenders.SelectedValue != null)
		{
			try
			{
				var changeableRecipient = _dbContext.Recipient.FirstOrDefault(x => x.RecipientId == _recipientId);
				
				if(changeableRecipient != default)
				{
					changeableRecipient.RecipientSeries = _txtSeries.Text;
					changeableRecipient.RecipientNumber = _txtNumber.Text;
					changeableRecipient.RecipientSurname = _txtSurname.Text;
					changeableRecipient.RecipientName = _txtName.Text;
					changeableRecipient.RecipientPatronymic = _txtPatronymic.Text;
					changeableRecipient.RecipientCity = (int)_cbxCity.SelectedValue;
					changeableRecipient.RecipientStreet = (int)_cbxStreet.SelectedValue;
					changeableRecipient.RecipientHome = _cbxHome.Text;
					changeableRecipient.RecipientApartment = int.Parse(_txtApartments.Text);
					changeableRecipient.RecipientPhone = _txtPhone.Text;
					changeableRecipient.RecipientSender = (int)_cbxSenders.SelectedValue;

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
			MessageBox.Show("Заполните пустые поля!");
		}
	}

	private void CbxCitiesSelectedIndexChanged(object sender, EventArgs e) => SettingCbxStreet();

	private void CbxStreetsSelectedIndexChanged(object sender, EventArgs e) => SettingCbxHome();

	private void CbxSendersSelectedIndexChanged(object sender, EventArgs e) => _lblSender.Text = _cbxSenders.SelectedValue?.ToString();

	private void TxtBoxesKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
			e.SuppressKeyPress = true;
	}

	private void OnCancelClick(object sender, EventArgs e) => Close();

	#endregion
}