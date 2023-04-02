using System.Data;

using PostSys.Models;
using PostSys.Application.ViewModels;

namespace PostSys.Application.Views.Forms;

/// <summary>Форма с адресами.</summary>
public partial class StreetCityForm : Form
{
	/// <summary>Создаёт экземпляр класса <see cref="StreetCityForm"/>.</summary>
	public StreetCityForm()
	{
		InitializeComponent();
	}


	#region Methods

	/// <summary>Загрузка таблицы с улицами.</summary>
	private void ShowTableStreet()
	{
		using PostSysContext db = new();

		_dgvStreets.DataSource = db.Streets.Select(x => new
		{
			ID = x.StreetId,
			Улица = x.StreetName,
		}).ToList();

		db.Dispose();

		_dgvStreets.Columns[0].Visible = false;
	}

	/// <summary>Загрузка таблицы с городами.</summary>
	private void ShowTableCity()
	{
		using PostSysContext db = new();

		_dgvCities.DataSource = db.Cities.Select(x => new
		{
			ID = x.CityId,
			Город = x.CityName
		}).ToList();

		db.Dispose();

		_dgvCities.Columns[0].Visible = false;
	}

	#endregion

	#region Handlers

	private void StreetCityFormLoad(object sender, EventArgs e)
		=> ShowTableStreet();

	private void TabControlSelectedIndexChanged(object sender, EventArgs e)
	{
		if(_tabControl.SelectedIndex == 0)
		{
			ShowTableStreet();
		}
		else
		{
			ShowTableCity();
		}
	}

	private void OnAddStreetClick(object sender, EventArgs e)
	{
		using PostSysContext db = new();

		try
		{
			if(!string.IsNullOrEmpty(_txtStreet.Text))
			{
				db.Streets.Add(new Street { StreetName = _txtStreet.Text });

				db.SaveChanges();
				db.Dispose();

				_txtStreet.Clear();
				ShowTableStreet();
			}

			db?.Dispose();
		}
		catch
		{
			db.Dispose();
			MessageBox.Show("Улица уже есть в таблице.");
		}
	}

	private void OnDeleteStreetClick(object sender, EventArgs e)
	{
		if(_dgvStreets.CurrentRow != null)
		{
			using PostSysContext db = new();

			db.Remove(db.Streets
				.Where(x => x.StreetId == (int)_dgvStreets.CurrentRow.Cells[0].Value)
				.Select(x => x).First());

			db.SaveChanges();
			db.Dispose();

			ShowTableStreet();
		}
		else
		{
			MessageBox.Show("В таблице нет данных.");
		}
	}

	private void OnAddCityClick(object sender, EventArgs e)
	{
		using PostSysContext db = new();

		try
		{
			if(!string.IsNullOrEmpty(_txtCity.Text))
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
			MessageBox.Show("Город уже есть в таблице.");
		}
	}

	private void OnDeleteCityClick(object sender, EventArgs e)
	{
		if(_dgvCities.CurrentRow != null)
		{
			using PostSysContext db = new();

			db.Remove(db.Cities
				.Where(x => x.CityId == (int)_dgvCities.CurrentRow.Cells[0].Value)
				.Select(x => x).First());

			db.SaveChanges();
			db.Dispose();

			ShowTableCity();
		}
		else
		{
			MessageBox.Show("В таблице нет данных.");
		}
	}

	#endregion

}