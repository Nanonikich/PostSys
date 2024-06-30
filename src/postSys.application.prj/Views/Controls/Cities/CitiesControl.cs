using Microsoft.IdentityModel.Tokens;

using PostSys.Application.Context;
using PostSys.Models;

namespace PostSys.Application.Views.Controls.StreetCity;

/// <summary>Связующий элемент управления, между формой и таблицей городов.</summary>
public partial class CitiesControl : UserControl
{
	private readonly PostSysContext _dbContext;
	private readonly DgvCitiesControl _dgvCitiesControl;

	/// <summary>Создаёт экземпляр класса <see cref="StreetsControl"/>.</summary>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	/// <param name="dgvCitiesControl">Элемент управления, содержащий таблицу городов.</param>
	public CitiesControl(PostSysContext dbContext, DgvCitiesControl dgvCitiesControl)
	{
		_dbContext = dbContext;

		_dgvCitiesControl = dgvCitiesControl;

		InitializeComponent();

		_pnlCitiesTable.Controls.Add(_dgvCitiesControl);
		_dgvCitiesControl.Dock = DockStyle.Fill;
	}

	/// <summary>Применяет заданное состояние загрузки информации в таблице.</summary>
	/// <param name="state"></param>
	public void LoadingState(bool state) => _dgvCitiesControl.TimerIsEnabledChanged.Invoke(this, state);

	private void OnAddCityClick(object sender, EventArgs e)
	{
		if(!_txtCity.Text.IsNullOrEmpty())
		{
			try
			{
				_dbContext.City.Add(new City { CityName = _txtCity.Text });
				_dbContext.SaveChanges();

				_txtCity.Clear();
			}
			catch
			{
				MessageBox.Show("Улица уже есть в таблице.");
			}
		}
	}

	private void OnDeleteCityClick(object sender, EventArgs e)
		=> _dgvCitiesControl.DeleteCurrentRow();

	public void DisposeEx() => _dgvCitiesControl.DisposeEx();
}
