using System.Data;

namespace postSys.application.prj.Views.Forms
{
	public partial class EditAddressForm : Form
	{
		#region Fields

		private readonly MainForm _mainForm;
		private int _mainId;

		#endregion

		#region .ctor

		public EditAddressForm(MainForm mainForm)
		{
			_mainForm = mainForm;

			InitializeComponent();
		}

		#endregion

		#region Methods

		private void SettingCbxRecipient()
		{
			using PostSysContext db = new();
			_cbxRecipient.DataSource = (from recipient in db.Recipients select recipient).ToList();
			_cbxRecipient.ValueMember = "RecipientId";
			_cbxRecipient.DisplayMember = "RecipientSurname";
			db.Dispose();
		}

		private void SettingCbxCity()
		{
			using PostSysContext db = new();

			_cbxCity.DataSource = (from cities in db.Cities
								   select cities).ToList();
			_cbxCity.ValueMember = "CityId";
			_cbxCity.DisplayMember = "CityName";
			db.Dispose();
		}

		private void SettingCbxStreet()
		{
			if (_cbxCity.SelectedValue != null)
			{
				using PostSysContext db = new();

				_cbxStreet.DataSource = (from codeAddress in db.CodesAddresses
										 where codeAddress.CodeAddressCity.ToString() == _cbxCity.SelectedValue.ToString()
										 select new {
											   codeAddress.CodeAddressId, 
											   codeAddress.CodeAddressCity, 
											   codeAddress.CodeAddressPlot, 
											   codeAddress.CodeAddressStreet,
											   codeAddress.CodeAddressStreetNavigation.StreetName,
											   codeAddress.CodeAddressHouses }).ToList();

				_cbxStreet.ValueMember = "CodeAddressStreet";
				_cbxStreet.DisplayMember = "StreetName";
				db.Dispose();
			}
		}

		private void SettingCbxHome()
		{
			using PostSysContext db = new();

			foreach (var item in (from addresses in db.CodesAddresses
								  select new { addresses.CodeAddressId, 
											   addresses.CodeAddressCity, 
											   addresses.CodeAddressPlot, 
											   addresses.CodeAddressStreetNavigation.StreetName, 
											   addresses.CodeAddressHouses }))
			{
				if (item.CodeAddressCity.ToString() == _cbxCity.SelectedValue.ToString() && item.StreetName.ToString() == _cbxStreet.Text.ToString())
				{
					if (item.CodeAddressHouses != null)
					{
						_cbxHome.DataSource = item.CodeAddressHouses.Split(", ").Select(x => Convert.ToString(x)).ToList();
					}
				}
			}
			
			db.Dispose();
		}

		private void SelectPlot()
		{
			using PostSysContext db = new();
			foreach (var item in (from addresses in db.CodesAddresses
								  select new { addresses.CodeAddressId, 
											   addresses.CodeAddressCity,
											   addresses.CodeAddressPlot, 
											   addresses.CodeAddressStreetNavigation.StreetName, 
											   addresses.CodeAddressHouses }))
			{
				foreach (var im in item.CodeAddressHouses)
				{
					if (item.CodeAddressCity.ToString() == _cbxCity.SelectedValue.ToString() &&
						item.StreetName.ToString() == _cbxStreet.Text.ToString() &&
						item.CodeAddressHouses.Contains(_cbxHome.Text))
					{
						_lblPlot.Text = item.CodeAddressPlot.ToString();
					}
				}
			}
		}

		private void SelectPostmen()
		{
			using PostSysContext db = new();
			_cbxPostmen.DataSource = (from postmens in db.Postmens
									  where postmens.PostmenPlot.ToString() == _lblPlot.Text.ToString()
									  select postmens).ToList();
			_cbxPostmen.ValueMember = "PostmenId";
			_cbxPostmen.DisplayMember = "PostmenSurname";
			db.Dispose();
		}

		private void SelectAddressForEdit()
		{
			if (_mainForm.ButtonEditClick == true)
			{
				_btnAdd.Visible = false;

				foreach (var item in _mainForm.DgvCurrentRow)
				{
					_mainId = item.AddressId;
					_lblPlot.Text = item.AddressPlot.ToString();
					_cbxRecipient.SelectedValue = item.AddressRecipient;
					_cbxCity.SelectedValue = item.AddressCity;
					_cbxStreet.SelectedValue = item.AddressStreet;
					_cbxHome.Text = item.AddressHome;
					_txtApartment.Text = item.AddressApartment;
					_cbxPostmen.SelectedValue = item.AddressPostmen;
					_txtGoods.Text = item.AddressGoods;
				}
			}
		}

		#endregion

		#region Events

		private void EditAddressForm_Load(object sender, EventArgs e)
		{
			SettingCbxRecipient();
			SettingCbxCity();
			SettingCbxStreet();
			SettingCbxHome();
			SelectPlot();
			SelectPostmen();

			SelectAddressForEdit();
		}

		private void OnAddClick(object sender, EventArgs e)
		{
			using PostSysContext db = new();
			try
			{
				if (!string.IsNullOrEmpty(_cbxRecipient.Text) && !string.IsNullOrEmpty(_cbxCity.Text) &&
					!string.IsNullOrEmpty(_cbxStreet.Text) && !string.IsNullOrEmpty(_cbxHome.Text) &&
					!string.IsNullOrEmpty(_txtApartment.Text) && !string.IsNullOrEmpty(_cbxPostmen.Text) &&
					!string.IsNullOrEmpty(_txtGoods.Text))
				{
					db.Addresses.Add(new Address
					{
						AddressPlot = Convert.ToInt32(_lblPlot.Text),
						AddressRecipient = Convert.ToInt32(_cbxRecipient.SelectedValue),
						AddressCity = Convert.ToInt32(_cbxCity.SelectedValue),
						AddressStreet = Convert.ToInt32(_cbxStreet.SelectedValue),
						AddressHome = _cbxHome.Text,
						AddressApartment = _txtApartment.Text,
						AddressPostmen = Convert.ToInt32(_cbxPostmen.SelectedValue),
						AddressGoods = _txtGoods.Text,
					});
					db.SaveChanges();
					db.Dispose();

					_mainForm.ShowTable();

					MessageBox.Show("Адрес добавлен");
					Close();
				}
				else
				{
					db.Dispose();
					MessageBox.Show("Заполните пустые поля");
				}
			}
			catch
			{
				MessageBox.Show("Неверные данные");
			}
		}

		private void OnEditClick(object sender, EventArgs e)
		{
			using PostSysContext db = new();
			try
			{
				if (!string.IsNullOrEmpty(_cbxRecipient.Text) && !string.IsNullOrEmpty(_cbxCity.Text) &&
				!string.IsNullOrEmpty(_cbxStreet.Text) && !string.IsNullOrEmpty(_cbxHome.Text) &&
				!string.IsNullOrEmpty(_txtApartment.Text) && !string.IsNullOrEmpty(_cbxPostmen.Text) &&
				!string.IsNullOrEmpty(_txtGoods.Text))
				{
					foreach (var item in (from addresses in db.Addresses
										  where addresses.AddressId.Equals(_mainId)
										  select addresses).ToList())
					{
						item.AddressPlot = Convert.ToInt32(_lblPlot.Text);
						item.AddressRecipient = Convert.ToInt32(_cbxRecipient.SelectedValue);
						item.AddressCity = Convert.ToInt32(_cbxCity.SelectedValue);
						item.AddressStreet = Convert.ToInt32(_cbxStreet.SelectedValue);
						item.AddressHome = _cbxHome.Text;
						item.AddressApartment = _txtApartment.Text;
						item.AddressPostmen = Convert.ToInt32(_cbxPostmen.SelectedValue);
						item.AddressGoods = _txtGoods.Text;
					}
					db.SaveChanges();
					db.Dispose();

					_mainForm.ShowTable();

					MessageBox.Show("Инфорация обновлена");
					Close();
				}
				else
				{
					db.Dispose();
					MessageBox.Show("Заполните пустые поля");
				}
			}
			catch
			{
				MessageBox.Show("Неверные данные");
			}
		}

		private void CbxCitySelectedIndexChanged(object sender, EventArgs e)
		{
			SettingCbxStreet();
			SelectPlot();
			SelectPostmen();
		}

		private void CbxStreetSelectedIndexChanged(object sender, EventArgs e)
		{
			SettingCbxHome();
			SelectPlot();
			SelectPostmen();
		}

		private void CbxHomeSelectedIndexChanged(object sender, EventArgs e)
		{
			SelectPlot();
			SelectPostmen();
		}

		/// <summary>
		/// Checking for Enter
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtBoxesKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
			}
		}

		private void OnCancelClick(object sender, EventArgs e) => Close();

		private void OnAddressFormClosed(object sender, FormClosedEventArgs e) => _mainForm.ButtonEditClick = false;

		#endregion
	}
}
