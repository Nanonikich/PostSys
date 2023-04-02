using System.Data;

using PostSys.Models;
using PostSys.Application.ViewModels;

namespace PostSys.Application.Views.Forms;

/// <summary>Форма выбора участка.</summary>
public partial class PlotsForm : Form
{
	/// <summary>Создаёт экземпляр класса <see cref="PlotsForm"/>.</summary>
	public PlotsForm()
	{
		InitializeComponent();
	}

	#region Methods

	/// <summary>Загрузка данных в таблицу.</summary>
	private void ShowTable()
	{
		using PostSysContext db = new();

		_dgvStreets.DataSource = db.CodesAddresses.Select(x => new
		{
			ID = x.CodeAddressId,
			Участок = x.CodeAddressPlot,
			Город = x.CodeAddressCityNavigation.CityName,
			Улица = x.CodeAddressStreetNavigation.StreetName,
			Дома = x.CodeAddressHouses
		}).ToList();

		db.Dispose();

		_dgvStreets.Columns[0].Visible = false;
	}

	/// <summary>Настройка ComboBoxes с городами и улицами.</summary>
	private void SettingComboBoxes()
	{
		using PostSysContext db = new();

		_cbxCity.DataSource = db.Cities.Select(x => x).ToList();
		_cbxCity.ValueMember = "CityId";
		_cbxCity.DisplayMember = "CityName";

		_cbxStreet.DataSource = db.Streets.Select(x => x).ToList();
		_cbxStreet.ValueMember = "StreetId";
		_cbxStreet.DisplayMember = "StreetName";

		db.Dispose();
	}

	#endregion

	#region Handlers

	private void PlotsFormLoad(object sender, EventArgs e)
	{
		SettingComboBoxes();

		ShowTable();
	}

	private void OnAddClick(object sender, EventArgs e)
	{
		using PostSysContext db = new();

		try
		{
			if(!string.IsNullOrEmpty(_txtPlot.Text) && !string.IsNullOrEmpty(_txtHouses.Text))
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
			MessageBox.Show("Неверный формат данных.");
		}
	}

	private void OnDeleteClick(object sender, EventArgs e)
	{
		try
		{
			if(_dgvStreets.CurrentRow != null)
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
				MessageBox.Show("В таблице нет данных.");
			}
		}
		catch
		{
			MessageBox.Show("Участок используется.");
		}
	}

	private void TxtPlotHomeKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
		{
			e.SuppressKeyPress = true;
		}
	}

	#endregion
}