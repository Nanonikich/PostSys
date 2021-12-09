using System.Data;

namespace postSys.application.prj.Views.Forms
{
	public partial class StreetCityForm : Form
	{
		#region .ctor

		public StreetCityForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		private void ShowTableStreet()
		{
			using PostSysContext db = new();

			_dgvStreets.DataSource = (from street in db.Streets
									  select new { 
										  ID = street.StreetId, 
										  Улица = street.StreetName 
												}).ToList();
			db.Dispose();

			_dgvStreets.Columns[0].Visible = false;
		}

		private void ShowTableCity()
		{
			using PostSysContext db = new();

			_dgvCities.DataSource = (from city in db.Cities
									 select new { 
										 ID = city.CityId, 
										 Город = city.CityName 
												}).ToList();
			db.Dispose();

			_dgvCities.Columns[0].Visible = false;
		}

		#endregion

		#region Events

		private void CitiesFormLoad(object sender, EventArgs e)
		{
			ShowTableStreet();
			ShowTableCity();
		}

		/// <summary>
		/// Working with streets
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnAddStreetClick(object sender, EventArgs e)
		{
			using PostSysContext db = new();
			try
			{
				if (!string.IsNullOrEmpty(_txtStreet.Text))
				{
					db.Streets.Add(new Street { StreetName = _txtStreet.Text });

					db.SaveChanges();
					db.Dispose();

					_txtStreet.Clear();
					ShowTableStreet();
				}
			}
			catch
			{
				db.Dispose();
				MessageBox.Show("Улица уже есть в таблице");
			}
		}

		private void OnDeleteStreetClick(object sender, EventArgs e)
		{
			if (_dgvStreets.CurrentRow != null)
			{
				using PostSysContext db = new();

				db.Remove((from street in db.Streets
						   where street.StreetId.ToString() == _dgvStreets.CurrentRow.Cells[0].Value.ToString()
						   select street).First());

				db.SaveChanges();
				db.Dispose();

				ShowTableStreet();
			}
			else
			{
				MessageBox.Show("В таблице нет данных");
			}
		}

		/// <summary>
		/// Working with cities
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnAddCityClick(object sender, EventArgs e)
		{
			using PostSysContext db = new();
			try
			{
				if (!string.IsNullOrEmpty(_txtCity.Text))
				{
					db.Cities.Add(new City { CityName = _txtCity.Text });

					db.SaveChanges();
					db.Dispose();

					_txtCity.Clear();

					ShowTableCity();
				}
			}
			catch
			{
				db.Dispose();
				MessageBox.Show("Город уже есть в таблице");
			}
		}

		private void OnDeleteCityClick(object sender, EventArgs e)
		{
			if (_dgvCities.CurrentRow != null)
			{
				using PostSysContext db = new();

				db.Remove((from city in db.Cities
						   where city.CityId.ToString() == _dgvCities.CurrentRow.Cells[0].Value.ToString()
						   select city).First());

				db.SaveChanges();
				db.Dispose();

				ShowTableCity();
			}
			else
			{
				MessageBox.Show("В таблице нет данных");
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

		#endregion
	}
}