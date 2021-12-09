using System.Data;

namespace postSys.application.prj.Views.Forms
{
	public partial class PlotsForm : Form
	{
		#region .ctor
		public PlotsForm()
		{
			InitializeComponent();
		}
		#endregion

		#region Methods

		private void ShowTable()
		{
			using PostSysContext db = new();

			_dgvStreets.DataSource = (from addresses in db.CodesAddresses
									  select new { ID = addresses.CodeAddressId, 
												   Участок  = addresses.CodeAddressPlot,
												   Город = addresses.CodeAddressCityNavigation.CityName,
												   Улица = addresses.CodeAddressStreetNavigation.StreetName, 
												   Дома = addresses.CodeAddressHouses }).ToList();
			db.Dispose();

			_dgvStreets.Columns[0].Visible = false;
		}

		private void SettingComboBoxes()
		{
			using PostSysContext db = new();

			// city Box
			_cbxCity.DataSource = (from cities in db.Cities
								   select cities).ToList();
			_cbxCity.ValueMember = "CityId";
			_cbxCity.DisplayMember = "CityName";

			// street Box
			_cbxStreet.DataSource = (from streets in db.Streets
									 select streets).ToList();
			_cbxStreet.ValueMember = "StreetId";
			_cbxStreet.DisplayMember = "StreetName";

			db.Dispose();
		}

		#endregion

		#region Events

		private void ServiceFormLoad(object sender, EventArgs e)
		{
			SettingComboBoxes();

			ShowTable();
		}

		private void OnAddClick(object sender, EventArgs e)
		{
			using PostSysContext db = new();

			try
			{
				if (!string.IsNullOrEmpty(_txtPlot.Text) && !string.IsNullOrEmpty(_txtHouses.Text))
				{
					db.CodesAddresses.Add(new CodeAddress
					{
						CodeAddressCity = Convert.ToInt32(_cbxCity.SelectedValue),
						CodeAddressPlot = Convert.ToInt32(_txtPlot.Text),
						CodeAddressStreet = Convert.ToInt32(_cbxStreet.SelectedValue),
						CodeAddressHouses = _txtHouses.Text
					});
					db.SaveChanges();
					db.Dispose();
					
					_txtPlot.Clear();
					_txtHouses.Clear();
					ShowTable();
				}
			}
			catch
			{
				db.Dispose();
				MessageBox.Show("Неверный формат данных");
			}
		}
		private void OnDeleteClick(object sender, EventArgs e)
		{
			try
			{
				if (_dgvStreets.CurrentRow != null)
				{
					using PostSysContext db = new();

					db.Remove((from addresses in db.CodesAddresses
							   where addresses.CodeAddressId == Convert.ToInt32(_dgvStreets.CurrentRow.Cells[0].Value)
							   select addresses).First());

					db.SaveChanges();
					db.Dispose();

					ShowTable();
				}
				else
				{
					MessageBox.Show("В таблице нет данных");
				}
			}
			catch
			{
				MessageBox.Show("Участок используется");
			}
		}

		/// <summary>
		/// Checking for Enter
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtPlotHomeKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
			}
		}

		#endregion
	}
}