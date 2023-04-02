using System.Data;

using PostSys.Models;
using PostSys.Application.ViewModels;

namespace PostSys.Application.Views.Forms.EditingForms;

/// <summary>Форма редактирования отправителей.</summary>
public partial class EditSenderForm : Form
{
	private int _senderId;
	private readonly SendersForm _sendersForm;

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="EditSenderForm"/>.</summary>
	/// <param name="sendersForm">Форма с отправителями.</param>
	public EditSenderForm(SendersForm sendersForm)
	{
		_sendersForm = sendersForm;

		InitializeComponent();
	}


	#endregion

	#region Methods

	/// <summary>Загрузка городов в ComboBox.</summary>
	private void SettingCbxCity()
	{
		using PostSysContext db = new();

		_cbxCity.DataSource = db.Cities.Select(x => x).ToList();

		_cbxCity.ValueMember = "CityId";
		_cbxCity.DisplayMember = "CityName";
		db.Dispose();
	}

	/// <summary>Загрузка улиц в ComboBox.</summary>
	private void SettingCbxStreet()
	{
		using PostSysContext db = new();

		_cbxStreet.DataSource = db.CodesAddresses
			.Where(x => x.CodeAddressCity.ToString() == _cbxCity.SelectedValue.ToString())
			.Select(x => new
			{
				x.CodeAddressId,
				x.CodeAddressCityNavigation.CityName,
				x.CodeAddressPlot,
				x.CodeAddressStreetNavigation.StreetName,
				x.CodeAddressHouses,
			}).ToList();

		_cbxStreet.ValueMember = "CodeAddressId";
		_cbxStreet.DisplayMember = "StreetName";
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
			if(item.CodeAddressCity.ToString() == _cbxCity.SelectedValue.ToString() && item.StreetName.ToString() == _cbxStreet.Text.ToString() && item.CodeAddressHouses != null)
			{
				_cbxHome.DataSource = item.CodeAddressHouses.Split(", ").Select(x => Convert.ToString(x)).ToList();
			}
		}
		db.Dispose();
	}

	/// <summary>Выбор отправителя для редактирования.</summary>
	private void SelectSenderForEdit()
	{
		if(_sendersForm.ButtonEditClick)
		{
			_btnAdd.Visible = false;

			foreach(var item in _sendersForm.DgvCurrentRow)
			{
				_senderId = item.SenderId;
				_txtSurname.Text = item.SenderSurname;
				_txtName.Text = item.SenderName;
				_txtPatronymic.Text = item.SenderPatronymic;
				_cbxCity.SelectedValue = item.SenderCity;
				_cbxStreet.SelectedValue = item.SenderStreet;
				_cbxHome.Text = item.SenderHome;
				_txtApartment.Text = item.SenderApartment.ToString();
				_txtPhone.Text = item.SenderPhone;
			}
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
		using PostSysContext db = new();

		if( !string.IsNullOrEmpty(_txtSurname.Text) && !string.IsNullOrEmpty(_txtName.Text) &&
			!string.IsNullOrEmpty(_cbxCity.Text) && !string.IsNullOrEmpty(_cbxStreet.Text) && 
			!string.IsNullOrEmpty(_cbxHome.Text) && !string.IsNullOrEmpty(_txtApartment.Text) && 
			!string.IsNullOrEmpty(_txtPhone.Text))
		{
			db.Senders.Add(new Sender
			{
				SenderSurname = _txtSurname.Text,
				SenderName = _txtName.Text,
				SenderPatronymic = _txtPatronymic.Text,
				SenderCity = Convert.ToInt32(_cbxCity.SelectedValue),
				SenderStreet = Convert.ToInt32(_cbxStreet.SelectedValue),
				SenderHome = _cbxHome.Text,
				SenderApartment = Convert.ToInt32(_txtApartment.Text),
				SenderPhone = _txtPhone.Text,
			});
			db.SaveChanges();
			db.Dispose();

			_sendersForm.ShowTable();

			MessageBox.Show("Отправитель добавлен.");
			Close();
		}
		else
		{
			db.Dispose();
			MessageBox.Show("Заполните пустые поля.");
		}
	}

	private void OnEditClick(object sender, EventArgs e)
	{
		using PostSysContext db = new();

		if( !string.IsNullOrEmpty(_txtSurname.Text) && !string.IsNullOrEmpty(_txtName.Text) &&
			!string.IsNullOrEmpty(_cbxCity.Text) && !string.IsNullOrEmpty(_cbxStreet.Text) && 
			!string.IsNullOrEmpty(_cbxHome.Text) && !string.IsNullOrEmpty(_txtApartment.Text) && 
			!string.IsNullOrEmpty(_txtPhone.Text))
		{
			foreach(var item in db.Senders
				.Where(x => x.SenderId.Equals(_senderId))
				.Select(x => x).ToList())
			{
				item.SenderSurname = _txtSurname.Text;
				item.SenderName = _txtName.Text;
				item.SenderPatronymic = _txtPatronymic.Text;
				item.SenderCity = Convert.ToInt32(_cbxCity.SelectedValue);
				item.SenderStreet = Convert.ToInt32(_cbxStreet.SelectedValue);
				item.SenderHome = _cbxHome.Text;
				item.SenderApartment = Convert.ToInt32(_txtApartment.Text);
				item.SenderPhone = _txtPhone.Text;
			}
			db.SaveChanges();
			db.Dispose();

			_sendersForm.ShowTable();

			MessageBox.Show("Инфорация обновлена.");
			Close();
		}
		else
		{
			db.Dispose();
			MessageBox.Show("Заполните пустые поля.");
		}
	}

	private void TxtBoxesKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
		{
			e.SuppressKeyPress = true;
		}
	}

	private void OnCbxCitySelectedIndexChanged(object sender, EventArgs e) => SettingCbxStreet();

	private void OnCbxStreetSelectedIndexChanged(object sender, EventArgs e) => SettingCbxHome();

	private void OnCancelClick(object sender, EventArgs e) => Close();

	private void OnSenderFormClosed(object sender, FormClosedEventArgs e) => _sendersForm.ButtonEditClick = false;

	#endregion
}