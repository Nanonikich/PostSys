using PostSys.Application.Views.Controls.StreetCity;

namespace PostSys.Application.Views.Forms;

/// <summary>Форма с адресами.</summary>
public partial class StreetCityForm : Form
{
	private readonly StreetsControl _streetsControl;
	private readonly CitiesControl _citiesControl;

	/// <summary>Создаёт экземпляр класса <see cref="StreetCityForm"/>.</summary>
	/// <param name="streetsControl">Элемент управления, содержащий действия над улицами.</param>
	/// <param name="citiesControl">Элемент управления, содержащий действия над городами.</param>
	public StreetCityForm(StreetsControl streetsControl, CitiesControl citiesControl)
	{
		_streetsControl = streetsControl;
		_citiesControl = citiesControl;

		InitializeComponent();


		tabPageOfStreets.Controls.Add(_streetsControl);
		_streetsControl.Dock = DockStyle.Fill;

		tabPageOfCities.Controls.Add(_citiesControl);
		_citiesControl.Dock = DockStyle.Fill;
	}

	#region Handlers

	private void TabControlSelectedIndexChanged(object sender, EventArgs e)
	{
		if(_tabControl.SelectedIndex == 0)
		{
			_citiesControl.LoadingState(false);
			_streetsControl.LoadingState(true);
		}
		else
		{
			_streetsControl.LoadingState(false);
			_citiesControl.LoadingState(true);
		}
	}

	private void StreetCityFormClosed(object sender, FormClosedEventArgs e)
	{
		_streetsControl.DisposeEx();
		_citiesControl.DisposeEx();
	}

	#endregion
}