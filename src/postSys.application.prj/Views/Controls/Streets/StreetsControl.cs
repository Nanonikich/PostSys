using Microsoft.IdentityModel.Tokens;

using PostSys.Application.Context;
using PostSys.Models;

namespace PostSys.Application.Views.Controls.StreetCity;

/// <summary>Связующий элемент управления, между формой и таблицей улиц.</summary>
public partial class StreetsControl : UserControl
{
	private readonly PostSysContext _dbContext;
	private readonly DgvStreetsControl _dgvStreetsControl;

	/// <summary>Создаёт экземпляр класса <see cref="StreetsControl"/>.</summary>
	/// <param name="dbContext">Контекст таблиц базы данных.</param>
	/// <param name="dgvStreetsControl">Элемент управления, содержащий таблицу улиц.</param>
	public StreetsControl(PostSysContext dbContext, DgvStreetsControl dgvStreetsControl)
	{
		_dbContext = dbContext;

		_dgvStreetsControl = dgvStreetsControl;

		InitializeComponent();

		_pnlStreetsTable.Controls.Add(_dgvStreetsControl);
		_dgvStreetsControl.Dock = DockStyle.Fill;
	}

	/// <summary>Применяет заданное состояние загрузки информации в таблицу с улицами.</summary>
	/// <param name="state"></param>
	public void LoadingState(bool state) => _dgvStreetsControl.TimerIsEnabledChanged.Invoke(this, state);

	private void OnAddStreetClick(object sender, EventArgs e)
	{
		if(!_txtStreet.Text.IsNullOrEmpty())
		{
			try
			{
				_dbContext.Street.Add(new Street { StreetName = _txtStreet.Text });
				_dbContext.SaveChanges();

				_txtStreet.Clear();
			}
			catch
			{
				MessageBox.Show("Улица уже есть в таблице.");
			}
		}
	}

	private void OnDeleteStreetClick(object sender, EventArgs e)
		=> _dgvStreetsControl.DeleteCurrentRow();

	public void DisposeEx() => _dgvStreetsControl.DisposeEx();
}
