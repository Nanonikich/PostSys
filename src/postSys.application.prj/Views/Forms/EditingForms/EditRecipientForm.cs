using System.Data;

using PostSys.Models;
using PostSys.Application.ViewModels;

namespace PostSys.Application.Views.Forms.EditingForms;

/// <summary>Форма редактирования информации о получателях.</summary>
public partial class EditRecipientForm : Form
{
	private int _recipientId;
	private readonly RecipientsForm _recipientsForm;

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="EditRecipientForm"/>.</summary>
	/// <param name="recipientsForm">Форма с информацией о получателях.</param>
	public EditRecipientForm(RecipientsForm recipientsForm)
	{
		_recipientsForm = recipientsForm;

		InitializeComponent();
	}

	#endregion

	#region Methods

	/// <summary>Загрузка городов в ComboBox.</summary>
	private void SettingCbxCity()
	{
		using PostSysContext db = new();

		_cbxCities.DataSource = db.Cities.Select(x => x).ToList();
		_cbxCities.ValueMember = "CityId";
		_cbxCities.DisplayMember = "CityName";

		db.Dispose();
	}

	/// <summary>Загрузка улиц в ComboBox.</summary>
	private void SettingCbxStreet()
	{
		using PostSysContext db = new();

		_cbxStreets.DataSource = db.CodesAddresses
			.Where(x => x.CodeAddressCity.ToString() == _cbxCities.SelectedValue.ToString())
			.Select(x => new
			{
				x.CodeAddressId,
				x.CodeAddressCityNavigation.CityName,
				x.CodeAddressPlot,
				x.CodeAddressStreetNavigation.StreetName,
				x.CodeAddressHouses
			}).ToList();

		_cbxStreets.ValueMember = "CodeAddressId";
		_cbxStreets.DisplayMember = "StreetName";
		db.Dispose();
	}

	/// <summary>Загрузка домов в ComboBox.</summary>
	private void SettingCbxHome()
	{
		using PostSysContext db = new();

		foreach(var item in db.CodesAddresses.Select(x => new
		{
			x.CodeAddressId,
			x.CodeAddressCity,
			x.CodeAddressPlot,
			x.CodeAddressStreetNavigation.StreetName,
			x.CodeAddressHouses,
		}))
		{
			if(item.CodeAddressCity.ToString() == _cbxCities.SelectedValue.ToString() && 
				item.StreetName.ToString() == _cbxStreets.Text.ToString())
			{
				if(item.CodeAddressHouses != null)
				{
					_cbxHome.DataSource = item.CodeAddressHouses.Split(", ").Select(x => Convert.ToString(x)).ToList();
				}
			}
		}
		db.Dispose();
	}

	/// <summary>Загрузка отправителей в ComboBox.</summary>
	private void SettingCbxSender()
	{
		using PostSysContext db = new();

		_cbxSenders.DataSource = db.Senders.Select(x => x).ToList();
		_cbxSenders.ValueMember = "SenderId";
		_cbxSenders.DisplayMember = "SenderSurname";
		db.Dispose();

		if(_cbxSenders.SelectedValue != null)
		{
			_lblSender.Text = _cbxSenders.SelectedValue.ToString();
		}
	}

	/// <summary>Выбор получателя для редактирования.</summary>
	private void SelectRecipientForEdit()
	{
		if(_recipientsForm.ButtonEditClick == true)
		{
			_btnAdd.Visible = false;

			foreach(var item in _recipientsForm.DgvCurrentRow)
			{
				_recipientId = item.RecipientId;
				_txtSeries.Text = item.RecipientSeries?.ToString();
				_txtNumber.Text = item.RecipientNumber?.ToString();
				_txtSurname.Text = item.RecipientSurname;
				_txtName.Text = item.RecipientName;
				_txtPatronymic.Text = item.RecipientPatronymic;
				_cbxCities.SelectedValue = item.RecipientCity;
				_cbxStreets.SelectedValue = item.RecipientStreet;
				_cbxHome.Text = item.RecipientHome;
				_txtApartments.Text = item.RecipientApartment.ToString();
				_txtPhone.Text = item.RecipientPhone;
				_cbxSenders.SelectedValue = item.RecipientSender;
			}
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
		using PostSysContext db = new();
		
		if( !string.IsNullOrEmpty(_txtSurname.Text) && !string.IsNullOrEmpty(_txtName.Text) &&
		    !string.IsNullOrEmpty(_cbxCities.Text) && !string.IsNullOrEmpty(_cbxStreets.Text) && 
			!string.IsNullOrEmpty(_cbxHome.Text) && !string.IsNullOrEmpty(_txtApartments.Text) && 
			!string.IsNullOrEmpty(_txtPhone.Text) && !string.IsNullOrEmpty(_cbxSenders.Text))
		{
			db.Recipients.Add(new Recipient
			{
				RecipientSeries = _txtSeries.Text,
				RecipientNumber = _txtNumber.Text,
				RecipientSurname = _txtSurname.Text,
				RecipientName = _txtName.Text,
				RecipientPatronymic = _txtPatronymic.Text,
				RecipientCity = Convert.ToInt32(_cbxCities.SelectedValue),
				RecipientStreet = Convert.ToInt32(_cbxStreets.SelectedValue),
				RecipientHome = _cbxHome.Text,
				RecipientApartment = Convert.ToInt32(_txtApartments.Text),
				RecipientPhone = _txtPhone.Text,
				RecipientSender = Convert.ToInt32(_cbxSenders.SelectedValue)
			});
			db.SaveChanges();
			db.Dispose();

			_recipientsForm.ShowTable();

			MessageBox.Show("Получатель добавлен.");
			Close();
		}
		else
		{
			db.Dispose();
			MessageBox.Show("Заполните пустые поля!");
		}

	}

	private void OnEditClick(object sender, EventArgs e)
	{
		using PostSysContext db = new();

		if( !string.IsNullOrEmpty(_txtSurname.Text) && !string.IsNullOrEmpty(_txtName.Text) &&
			!string.IsNullOrEmpty(_cbxCities.Text) && !string.IsNullOrEmpty(_cbxStreets.Text) && 
			!string.IsNullOrEmpty(_cbxHome.Text) && !string.IsNullOrEmpty(_txtApartments.Text) && 
			!string.IsNullOrEmpty(_txtPhone.Text) && !string.IsNullOrEmpty(_cbxSenders.Text))
		{
			foreach(var item in db.Recipients
				.Where(x => x.RecipientId.Equals(_recipientId))
				.Select(x => x).ToList())
			{
				item.RecipientSeries = _txtSeries.Text;
				item.RecipientNumber = _txtNumber.Text;
				item.RecipientSurname = _txtSurname.Text;
				item.RecipientName = _txtName.Text;
				item.RecipientPatronymic = _txtPatronymic.Text;
				item.RecipientCity = Convert.ToInt32(_cbxCities.SelectedValue);
				item.RecipientStreet = Convert.ToInt32(_cbxStreets.SelectedValue);
				item.RecipientHome = _cbxHome.Text;
				item.RecipientApartment = Convert.ToInt32(_txtApartments.Text);
				item.RecipientPhone = _txtPhone.Text;
				item.RecipientSender = Convert.ToInt32(_cbxSenders.SelectedValue);
			}
			db.SaveChanges();
			db.Dispose();

			_recipientsForm.ShowTable();

			MessageBox.Show("Информация обновлена.");
			Close();
		}
		else
		{
			db.Dispose();
			MessageBox.Show("Заполните пустые поля!");
		}
	}

	private void CbxCitiesSelectedIndexChanged(object sender, EventArgs e) => SettingCbxStreet();

	private void CbxStreetsSelectedIndexChanged(object sender, EventArgs e) => SettingCbxHome();

	private void CbxSendersSelectedIndexChanged(object sender, EventArgs e) => _lblSender.Text = _cbxSenders.SelectedValue.ToString();

	private void TxtBoxesKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
		{
			e.SuppressKeyPress = true;
		}
	}

	private void OnCancelClick(object sender, EventArgs e) => Close();

	private void OnRecipientFormClosed(object sender, FormClosedEventArgs e) => _recipientsForm.ButtonEditClick = false;

	#endregion
}