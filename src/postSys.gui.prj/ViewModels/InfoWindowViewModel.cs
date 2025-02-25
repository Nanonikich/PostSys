using ReactiveUI;

using PostSys.Gui.Common.ViewModels;

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления окна "О программе".</summary>
public class InfoWindowViewModel : ViewModelBase
{
	#region Data

	private string _version = "Version: 1.0.0"; // TODO: запрашивать версию

	#endregion

	#region Properties

	/// <summary>Возвращает или задаёт версию программы.</summary>
	/// <value>Версия программы.</value>
	public string Version
	{
		get => _version;
		set => this.RaiseAndSetIfChanged(ref _version, value);
	}

	#endregion
}
