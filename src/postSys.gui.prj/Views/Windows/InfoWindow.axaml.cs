using Avalonia.ReactiveUI;

using PostSys.Gui.ViewModels;

namespace PostSys.Gui.Views.Windows;

public partial class InfoWindow : ReactiveWindow<InfoWindowViewModel>
{
	public InfoWindow()
	{
		InitializeComponent();
	}
}