using Microsoft.IdentityModel.Tokens;

using Serilog;

using PostSys.Application.Context;
using PostSys.Models;

namespace PostSys.Application.Views.Controls.Plots;

/// <summary>Элемент управления, отвечающий за добавление/удаление участков.</summary>
public partial class PlotOptionsControl : UserControl
{
	private static readonly ILogger Log = Serilog.Log.ForContext<PlotOptionsControl>();

	private readonly PostSysContext _dbContext;
	private object? _selectedCityForRefresh;
	private object? _selectedStreetForRefresh;

	/// <summary>Событие удаления, выбранного в таблице, участка.</summary>
	public EventHandler<bool> PlotRemoving = delegate { };

	/// <summary>Создаёт экземпляр класса <see cref="PlotOptionsControl"/>.</summary>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	public PlotOptionsControl(PostSysContext dbContext)
	{
		_dbContext = dbContext;

		InitializeComponent();
	}

	/// <summary>Загрузка городов в ComboBox.</summary>
	public void SettingCbxCity()
	{
		_selectedCityForRefresh = _cbxCity.SelectedValue;

		_cbxCity.DataSource = _dbContext.City.ToList();
		_cbxCity.ValueMember = "CityId";
		_cbxCity.DisplayMember = "CityName";

		if(_selectedCityForRefresh != null)
			_cbxCity.SelectedValue = _selectedCityForRefresh;
	}

	/// <summary>Загрузка улиц в ComboBox.</summary>
	public void SettingCbxStreet()
	{
		_selectedStreetForRefresh = _cbxStreet.SelectedValue;

		_cbxStreet.DataSource = _dbContext.Street.ToList();
		_cbxStreet.ValueMember = "StreetId";
		_cbxStreet.DisplayMember = "StreetName";

		if(_selectedStreetForRefresh != null)
			_cbxStreet.SelectedValue = _selectedStreetForRefresh;
	}

	#region Handlers

	private void OnAddClick(object sender, EventArgs e)
	{
		if(!_txtPlot.Text.IsNullOrEmpty() && !_txtHouses.Text.IsNullOrEmpty() &&
			_cbxCity.SelectedValue != null && _cbxStreet.SelectedValue != null)
		{
			try
			{
				_dbContext.AddressCode.Add(new AddressCode
				{
					AddressCodeCity = (int)_cbxCity.SelectedValue,
					AddressCodePlot = int.Parse(_txtPlot.Text),
					AddressCodeStreet = (int)_cbxStreet.SelectedValue,
					AddressCodeHouses = _txtHouses.Text,
				});
				_dbContext.SaveChanges();

				_txtPlot.Clear();
				_txtHouses.Clear();
			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				MessageBox.Show("Неверный формат данных.");
			}
		}
		else
		{
			MessageBox.Show("Заполнены не все поля.");
		}
	}

	private void OnDeleteClick(object sender, EventArgs e) 
		=> PlotRemoving.Invoke(this, true);

	private void TxtPlotHousesKeyDown(object sender, KeyEventArgs e)
	{
		if(e.KeyCode == Keys.Enter)
			e.SuppressKeyPress = true;
	}

	#endregion
}
