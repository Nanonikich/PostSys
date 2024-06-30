using Microsoft.IdentityModel.Tokens;

using Serilog;

using PostSys.Application.Context;
using PostSys.Models;

namespace PostSys.Application.Views.Forms.EditingForms;

/// <summary>Форма редактирования адресов.</summary>
public partial class EditAddressForm : Form
{
	private static readonly ILogger Log = Serilog.Log.ForContext<EditAddressForm>();

	private readonly PostSysContext _dbContext;
	private int _mainId;

	/// <summary>Выбранный адрес для редактирования.</summary>
	public Address? CurrentAddress { get; set; }

	/// <summary>Создаёт экземпляр класса <see cref="EditAddressForm"/>.</summary>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	public EditAddressForm(PostSysContext dbContext)
	{
		_dbContext = dbContext;

		InitializeComponent();
	}

	#region Methods

	/// <summary>Загрузка данных о получателях в ComboBox.</summary>
	private void SettingCbxRecipient()
	{
		_cbxRecipient.DataSource = _dbContext.Recipient.ToList();
		_cbxRecipient.ValueMember = "RecipientId";
		_cbxRecipient.DisplayMember = "RecipientSurname";
	}

	/// <summary>Загрузка данных о городах в ComboBox.</summary>
	private void SettingCbxCity()
	{
		_cbxCity.DataSource = _dbContext.City.ToList();
		_cbxCity.ValueMember = "CityId";
		_cbxCity.DisplayMember = "CityName";
	}

	/// <summary>Настройка списка улиц в ComboBox.</summary>
	private void SettingCbxStreet()
	{
		if(_cbxCity.SelectedValue != null)
		{
			_cbxStreet.DataSource = _dbContext.AddressCode
				.Where(x => x.AddressCodeCity.ToString() == _cbxCity.SelectedValue.ToString())
				.Select(x => new
				{
					x.AddressCodeId,
					x.AddressCodeCity,
					x.AddressCodePlot,
					x.AddressCodeStreet,
					x.AddressCodeStreetNavigation.StreetName,
					x.AddressCodeHouses,
				}).ToList();

			_cbxStreet.ValueMember = "AddressCodeStreet";
			_cbxStreet.DisplayMember = "StreetName";
		}
	}

	/// <summary>Настройка ComboBox с домами.</summary>
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

	/// <summary>Выбор участка.</summary>
	private void SelectPlot()
	{
		if(_cbxCity.SelectedValue != null && !_cbxStreet.Text.IsNullOrEmpty() && !_cbxHome.Text.IsNullOrEmpty())
		{
			_lblPlot.Text = _dbContext.AddressCode
				.Where(x => x.AddressCodeCity.ToString() == _cbxCity.SelectedValue.ToString() &&
					x.AddressCodeStreetNavigation.StreetName == _cbxStreet.Text &&
					x.AddressCodeHouses.Contains(_cbxHome.Text))
				.Select(x => x.AddressCodePlot.ToString()).FirstOrDefault();
		}
	}

	/// <summary>Выбор почтальона в ComboBox.</summary>
	private void SelectPostman()
	{
		if(!_lblPlot.Text.IsNullOrEmpty() && int.TryParse(_lblPlot.Text, out int plot))
		{
			_cbxPostman.DataSource = _dbContext.Postman.Where(x => x.PostmanPlot == plot).ToList();

			_cbxPostman.ValueMember = "PostmanId";
			_cbxPostman.DisplayMember = "PostmanSurname";
		}
	}

	/// <summary>Выбор адреса для редактирования.</summary>
	private void SelectAddressForEdit()
	{
		if(CurrentAddress != null)
		{
			_btnAdd.Visible = false;

			_mainId = CurrentAddress.AddressId;
			_lblPlot.Text = CurrentAddress.AddressPlot.ToString();
			_cbxRecipient.SelectedValue = CurrentAddress.AddressRecipient;
			_cbxCity.SelectedValue = CurrentAddress.AddressCity;
			_cbxStreet.SelectedValue = CurrentAddress.AddressStreet;
			_cbxHome.Text = CurrentAddress.AddressHome;
			_txtApartment.Text = CurrentAddress.AddressApartment;
			_cbxPostman.SelectedValue = CurrentAddress.AddressPostman;
			_txtGoods.Text = CurrentAddress.AddressGoods;
		}
	}

	#endregion

	#region Handlers

	private void EditAddressFormLoad(object sender, EventArgs e)
	{
		SettingCbxRecipient();
		SettingCbxCity();
		SettingCbxStreet();
		SettingCbxHome();

		SelectPlot();
		SelectPostman();

		SelectAddressForEdit();
	}

	private void OnAddClick(object sender, EventArgs e)
	{
		if(_cbxRecipient.SelectedValue != null && _cbxCity.SelectedValue != null &&
			_cbxStreet.SelectedValue != null && !_cbxHome.Text.IsNullOrEmpty() &&
			!_txtApartment.Text.IsNullOrEmpty() && _cbxPostman.SelectedValue != null &&
			!_txtGoods.Text.IsNullOrEmpty())
		{
			try
			{
				_dbContext.Address.Add(new Address
				{
					AddressPlot = int.Parse(_lblPlot.Text),
					AddressRecipient = (int)_cbxRecipient.SelectedValue,
					AddressCity = (int)_cbxCity.SelectedValue,
					AddressStreet = (int)_cbxStreet.SelectedValue,
					AddressHome = _cbxHome.Text,
					AddressApartment = _txtApartment.Text,
					AddressPostman = (int)_cbxPostman.SelectedValue,
					AddressGoods = _txtGoods.Text,
				});
				_dbContext.SaveChanges();

				MessageBox.Show("Адрес добавлен.");
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
		if(_cbxRecipient.SelectedValue != null && _cbxCity.SelectedValue != null &&
			_cbxStreet.SelectedValue != null && !_cbxHome.Text.IsNullOrEmpty() &&
			!_txtApartment.Text.IsNullOrEmpty() && _cbxPostman.SelectedValue != null &&
			!_txtGoods.Text.IsNullOrEmpty())
		{
			try
			{
				var changeableAddress = _dbContext.Address.FirstOrDefault(x => x.AddressId == _mainId);

				if(changeableAddress != default)
				{
					changeableAddress.AddressPlot = int.Parse(_lblPlot.Text);
					changeableAddress.AddressRecipient = (int)_cbxRecipient.SelectedValue;
					changeableAddress.AddressCity = (int)_cbxCity.SelectedValue;
					changeableAddress.AddressStreet = (int)_cbxStreet.SelectedValue;
					changeableAddress.AddressHome = _cbxHome.Text;
					changeableAddress.AddressApartment = _txtApartment.Text;
					changeableAddress.AddressPostman = (int)_cbxPostman.SelectedValue;
					changeableAddress.AddressGoods = _txtGoods.Text;

					_dbContext.SaveChanges();

					MessageBox.Show("Информация обновлена.");
					Close();
				}
				else
				{
					MessageBox.Show("Выбранный адрес не найден.");
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

	private void CbxCitySelectedIndexChanged(object sender, EventArgs e)
	{
		SettingCbxStreet();
		SelectPlot();
		SelectPostman();
	}

	private void CbxStreetSelectedIndexChanged(object sender, EventArgs e)
	{
		SettingCbxHome();
		SelectPlot();
		SelectPostman();
	}

	private void CbxHomeSelectedIndexChanged(object sender, EventArgs e)
	{
		SelectPlot();
		SelectPostman();
	}

	private void TxtBoxesKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
			e.SuppressKeyPress = true;
	}

	private void OnCancelClick(object sender, EventArgs e) => Close();

	#endregion
}
