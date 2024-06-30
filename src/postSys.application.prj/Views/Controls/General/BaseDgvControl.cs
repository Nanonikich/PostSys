using PostSys.Application.Context;

namespace PostSys.Application.Views.Controls;

/// <summary>Базовый элемент управления таблицами.</summary>
public partial class BaseDgvControl : UserControl
{
	#region Properties

	/// <summary>Контекст базы данных.</summary>
	protected PostSysContext DbContext { get; set; }

	/// <summary>Выбранный ряд таблицы.</summary>
	protected int CurrentId { get; set; }

	/// <summary>Выбранный ряд таблицы.</summary>
	public DataGridViewRow? CurrentRow => _dgvClassBase.CurrentRow;

	#region Elements

	/// <summary>Таблица.</summary>
	protected DataGridView DataGrid => _dgvClassBase;

	/// <summary>Таймер.</summary>
	protected System.Windows.Forms.Timer Timer => _timer;

	#endregion

	#endregion

	/// <summary>Событие изменения состояния таймера.</summary>
	public EventHandler<bool> TimerIsEnabledChanged = delegate { };

	/// <summary>Создаёт экземпляр класса <see cref="BaseDgvControl"/>.</summary>
	public BaseDgvControl()
	{
		TimerIsEnabledChanged += OnTimerStateChanged;

		InitializeComponent();
	}

	/// <summary>Загрузка данных в таблицу.</summary>
	public virtual void LoadingTableData()
	{

	}

	/// <summary>Закрашивание выбранного ряда.</summary>
	protected void ShadingCurrentRow()
	{
		var currentRow = DataGrid.Rows
			.Cast<DataGridViewRow>()
			.FirstOrDefault(row => (int)row.Cells[0].Value == CurrentId);

		if(currentRow != null)
		{
			DataGrid.CurrentCell = currentRow.Cells[0].Visible != false ? 
				DataGrid.CurrentCell = currentRow.Cells[0]
				: DataGrid.CurrentCell = currentRow.Cells[1];
		}
	}

	private void BaseDgvControlLoad(object sender, EventArgs e) => _timer.Start();

	private void OnTimerTick(object sender, EventArgs e) => LoadingTableData();

	private void OnTimerStateChanged(object? sender, bool isEnabled)
	{
		if(!isEnabled && _timer.Enabled)
		{
			_timer.Stop();
		}
		else if(isEnabled && !_timer.Enabled)
		{
			_timer.Start();
		}
	}

	#region DisposableEx

	public bool IsDisposed { get; private set; }

	public virtual void DisposeEx()
	{
		if(!IsDisposed)
		{
			_timer.Stop();

			TimerIsEnabledChanged -= OnTimerStateChanged;

			IsDisposed = true;
		}
	}

	#endregion

}
