using System.Data;

namespace postSys.application.prj.Views.Forms
{
	public partial class EditRecipientForm : Form
	{
		#region Fields

		private readonly RecipientsForm _recipientsForm;
		private int _recipientId;

		#endregion

		#region .ctor

		public EditRecipientForm(RecipientsForm recipientsForm)
		{
			_recipientsForm = recipientsForm;

			InitializeComponent();
		}

		#endregion

		#region Methods

		private void SettingCbxCity()
		{
			using PostSysContext db = new();

			_cbxCities.DataSource = (from cities in db.Cities
								     select cities).ToList();
			_cbxCities.ValueMember = "CityId";
			_cbxCities.DisplayMember = "CityName";
			db.Dispose();
		}

		private void SettingCbxStreet()
		{
			using PostSysContext db = new();

			_cbxStreets.DataSource = (from addresses in db.CodesAddresses
									  where addresses.CodeAddressCity.ToString() == _cbxCities.SelectedValue.ToString()
									  select new { addresses.CodeAddressId, 
												   addresses.CodeAddressCityNavigation.CityName, 
												   addresses.CodeAddressPlot, 
												   addresses.CodeAddressStreetNavigation.StreetName, 
												   addresses.CodeAddressHouses }).ToList();
			_cbxStreets.ValueMember = "CodeAddressId";
			_cbxStreets.DisplayMember = "StreetName";
			db.Dispose();
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
				if (item.CodeAddressCity.ToString() == _cbxCities.SelectedValue.ToString() && 
					item.StreetName.ToString() == _cbxStreets.Text.ToString())
				{
					if (item.CodeAddressHouses != null)
					{
						_cbxHome.DataSource = item.CodeAddressHouses.Split(", ").Select(x => Convert.ToString(x)).ToList();
					}
				}
			}
			db.Dispose();
		}

		private void SettingCbxSender()
		{
			using PostSysContext db = new();

			_cbxSenders.DataSource = (from senders in db.Senders
									  select senders).ToList();
			_cbxSenders.ValueMember = "SenderId";
			_cbxSenders.DisplayMember = "SenderSurname";
			db.Dispose();

			if(_cbxSenders.SelectedValue != null)
			{
				_lblSender.Text = _cbxSenders.SelectedValue.ToString();
			}
		}

		private void SelectRecipientForEdit()
		{
			if (_recipientsForm.ButtonEditClick == true)
			{
				_btnAdd.Visible = false;

				foreach (var item in _recipientsForm.DgvCurrentRow)
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

		#region Events

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
			
			if (!string.IsNullOrEmpty(_txtSurname.Text) && !string.IsNullOrEmpty(_txtName.Text) &&
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

				MessageBox.Show("Получатель добавлен");
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
				!string.IsNullOrEmpty(_cbxCities.Text) && !string.IsNullOrEmpty(_cbxStreets.Text) && 
				!string.IsNullOrEmpty(_cbxHome.Text) && !string.IsNullOrEmpty(_txtApartments.Text) && 
				!string.IsNullOrEmpty(_txtPhone.Text) && !string.IsNullOrEmpty(_cbxSenders.Text))
			{
				foreach (var item in (from recipients in db.Recipients
									  where recipients.RecipientId.Equals(_recipientId)
									  select recipients).ToList())
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

				MessageBox.Show("Информация обновлена");
				Close();
			}
			else
			{
				db.Dispose();
				MessageBox.Show("Заполните пустые поля");
			}
		}

		private void CbxCitiesSelectedIndexChanged(object sender, EventArgs e) => SettingCbxStreet();

		private void CbxStreetsSelectedIndexChanged(object sender, EventArgs e) => SettingCbxHome();

		private void CbxSendersSelectedIndexChanged(object sender, EventArgs e) => _lblSender.Text = _cbxSenders.SelectedValue.ToString();

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

		private void OnRecipientFormClosed(object sender, FormClosedEventArgs e) => _recipientsForm.ButtonEditClick = false;

		#endregion
	}
}