using ReactiveUI.Avalonia;

using PostSys.Gui.ViewModels;

namespace PostSys.Gui.Views.Windows;

/// <summary>Главное окно desktop-хоста.</summary>
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
	public MainWindow()
	{
		InitializeComponent();
	}
}
