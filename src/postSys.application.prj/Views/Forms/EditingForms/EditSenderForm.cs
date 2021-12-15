using System.Data;

namespace postSys.application.prj.Views.Forms
{
	public partial class EditSenderForm : Form
	{
		#region Fields

		private readonly SendersForm _sendersForm;
		private int _senderId;

		#endregion

		#region .ctor

		public EditSenderForm(SendersForm sendersForm)
		{
			_sendersForm = sendersForm;

			InitializeComponent();
		}


		#endregion

		#region Methods

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
			using PostSysContext db = new();

			_cbxStreet.DataSource = (from addresses in db.CodesAddresses
									 where addresses.CodeAddressCity.ToString() == _cbxCity.SelectedValue.ToString()
									 select new { addresses.CodeAddressId, 
												  addresses.CodeAddressCityNavigation.CityName, 
												  addresses.CodeAddressPlot, 
												  addresses.CodeAddressStreetNavigation.StreetName, 
												  addresses.CodeAddressHouses }).ToList();
			_cbxStreet.ValueMember = "CodeAddressId";
			_cbxStreet.DisplayMember = "StreetName";
			db.Dispose();
		}

		private void SettingCbxHome()
		{
			using PostSysContext db = new();

			foreach (var item in from addresses in db.CodesAddresses
								 select new { addresses.CodeAddressId, 
											  addresses.CodeAddressCity, 
											  addresses.CodeAddressPlot, 
											  addresses.CodeAddressStreetNavigation.StreetName, 
											  addresses.CodeAddressHouses })
			{
				if (item.CodeAddressCity.ToString() == _cbxCity.SelectedValue.ToString() && item.StreetName.ToString() == _cbxStreet.Text.ToString() && item.CodeAddressHouses != null)
				{
					_cbxHome.DataSource = item.CodeAddressHouses.Split(", ").Select(x => Convert.ToString(x)).ToList();
				}
			}
			db.Dispose();
		}

		private void SelectSenderForEdit()
		{
			if (_sendersForm.ButtonEditClick)
			{
				_btnAdd.Visible = false;

				foreach (var item in _sendersForm.DgvCurrentRow)
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

		#region Events

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

			if (!string.IsNullOrEmpty(_txtSurname.Text) && !string.IsNullOrEmpty(_txtName.Text) &&
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

				MessageBox.Show("Отправитель добавлен");
				Close();
			}
			else
			{
				db.Dispose();
				MessageBox.Show("Заполните пустые поля");
			}
		}

		private void OnEditClick(object sender, EventArgs e)
		{
			using PostSysContext db = new();

			if (!string.IsNullOrEmpty(_txtSurname.Text) && !string.IsNullOrEmpty(_txtName.Text) &&
				!string.IsNullOrEmpty(_cbxCity.Text) && !string.IsNullOrEmpty(_cbxStreet.Text) && 
				!string.IsNullOrEmpty(_cbxHome.Text) && !string.IsNullOrEmpty(_txtApartment.Text) && 
				!string.IsNullOrEmpty(_txtPhone.Text))
			{
				foreach (var item in (from senders in db.Senders
									  where senders.SenderId.Equals(_senderId)
									  select senders).ToList())
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

				MessageBox.Show("Инфорация обновлена");
				Close();
			}
			else
			{
				db.Dispose();
				MessageBox.Show("Заполните пустые поля");
			}
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

		private void OnCbxCitySelectedIndexChanged(object sender, EventArgs e) => SettingCbxStreet();

		private void OnCbxStreetSelectedIndexChanged(object sender, EventArgs e) => SettingCbxHome();

		private void OnCancelClick(object sender, EventArgs e) => Close();

		private void OnSenderFormClosed(object sender, FormClosedEventArgs e) => _sendersForm.ButtonEditClick = false;

		#endregion
	}
}