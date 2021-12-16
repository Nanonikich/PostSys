using mUse.application.prj.Views.Forms;
using System.Data;

namespace postSys.application.prj.Views.Forms
{
	public partial class MainForm : Form
	{
		#region Properties
		
		public bool ButtonEditClick { get; set; }
		public List<Address> DgvCurrentRow { get; set; } = null!;
		private string ?CurrentRowInDgv { get; set; }

		#endregion

		#region .ctor

		public MainForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		public void ShowTable()
		{
			if (_dgvAddresses.CurrentRow != null)
			{
				CurrentRowInDgv = _dgvAddresses.CurrentRow.Cells[1].Value.ToString();
			}

			timer.Start();

			using PostSysContext db = new();

			_dgvAddresses.DataSource = (from address in db.Addresses
										let addressPostmenNavigation = address.AddressPostmenNavigation
										join city in db.Cities on address.AddressCity equals city.CityId
										join street in db.Streets on address.AddressStreet equals street.StreetId
										select new
										{
											ID = address.AddressId,
											Участок = addressPostmenNavigation.PostmenPlot,
											Получатель = address.AddressRecipientNavigation.RecipientSurname,
											Город = city.CityName,
											Улица = street.StreetName,
											Дом = address.AddressHome,
											Квартира = address.AddressApartment,
											Почтальон = addressPostmenNavigation.PostmenSurname,
											Товары = address.AddressGoods

										}).ToList();
			db.Dispose();

			if (_dgvAddresses != null)
			{
				// закрашивание ряда
				foreach (DataGridViewRow row in _dgvAddresses.Rows)
				{
					if (row.Cells[1].Value.ToString().Equals(CurrentRowInDgv))
					{
						_dgvAddresses.CurrentCell = row.Cells[1];
					}
				}
			}
		}

		#endregion

		#region Events

		private void MainFormLoad(object sender, EventArgs e) => ShowTable();

		private void OnAddClick(object sender, EventArgs e) => new EditAddressForm(this).ShowDialog();

		private void OnEditClick(object sender, EventArgs e)
		{
			if (_dgvAddresses.CurrentRow != null)
			{
				ButtonEditClick = true;

				using PostSysContext db = new();
				DgvCurrentRow = (from addresses in db.Addresses
								 where addresses.AddressId.ToString() == _dgvAddresses.CurrentRow.Cells[0].Value.ToString()
								 select addresses).ToList();
				db.Dispose();

				new EditAddressForm(this).ShowDialog();
			}
			else
			{
				MessageBox.Show("В таблице нет данных");
			}
		}

		private void OnDeleteClick(object sender, EventArgs e)
		{
			if (_dgvAddresses.CurrentRow != null)
			{
				using PostSysContext db = new();

				db.Remove((from address in db.Addresses
						   where address.AddressId.ToString() == _dgvAddresses.CurrentRow.Cells[0].Value.ToString()
						   select address).First());

				db.SaveChanges();
				db.Dispose();

				ShowTable();
			}
			else
			{
				MessageBox.Show("В таблице нет данных");
			}
		}

		/// <summary>
		/// Left Buttons
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnPostmensClick(object sender, EventArgs e) => new PostmensForm().ShowDialog();

		private void OnSendersClick(object sender, EventArgs e) => new SendersForm().ShowDialog();

		private void OnClientsClick(object sender, EventArgs e) => new RecipientsForm().ShowDialog();

		private void OnCitiesClick(object sender, EventArgs e) => new StreetCityForm().ShowDialog();

		private void OnPlotsClick(object sender, EventArgs e) => new PlotsForm().ShowDialog();

		private void OnTimerTick(object sender, EventArgs e) => ShowTable();

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e) => Environment.Exit(0);

		#endregion
	}
}