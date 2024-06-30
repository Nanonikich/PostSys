using Serilog;

using PostSys.Application.Context;
using PostSys.Application.Views.Controls;
using PostSys.Application.Views.Controls.Plots;

namespace PostSys.Application.Views.Forms;

/// <summary>Форма редактирования участков.</summary>
public partial class PlotsForm : Form
{
	private static readonly ILogger Log = Serilog.Log.ForContext<PlotsForm>();

	private readonly PostSysContext _dbContext;
	private readonly DgvPlotsControl _dgvPlotsControl;
	private readonly PlotOptionsControl _plotOptionsControl;

	/// <summary>Создаёт экземпляр класса <see cref="PlotsForm"/>.</summary>
	/// <param name="dgvPlotsControl">Элемент управления, содержащий таблицу участков.</param>
	/// <param name="plotOptionsControl">Элемент управления, содержащий настройку над участками.</param>
	public PlotsForm(DgvPlotsControl dgvPlotsControl, PlotOptionsControl plotOptionsControl)
	{
		_dgvPlotsControl = dgvPlotsControl;
		_plotOptionsControl = plotOptionsControl;

		_plotOptionsControl.PlotRemoving += OnPlotRemove;

		InitializeComponent();

		_pnlPlots.Controls.Add(_dgvPlotsControl);
		_dgvPlotsControl.Dock = DockStyle.Fill;

		_pnlPlotOptions.Controls.Add(_plotOptionsControl);
		_plotOptionsControl.Dock = DockStyle.Fill;
	}

	#region Handlers

	private void PlotsFormLoad(object sender, EventArgs e) => _timer.Start();

	private void OnPlotRemove(object? sender, bool e)
		=> _dgvPlotsControl.DeleteCurrentRow();

	private void OnTimerTick(object sender, EventArgs e)
	{
		_plotOptionsControl.SettingCbxCity();
		_plotOptionsControl.SettingCbxStreet();
		_dgvPlotsControl.LoadingTableData();
	}

	private void PlotsFormClosed(object sender, FormClosedEventArgs e)
	{
		_dgvPlotsControl.DisposeEx();

		_timer.Stop();
		_plotOptionsControl.PlotRemoving -= OnPlotRemove;
	}

	#endregion

}