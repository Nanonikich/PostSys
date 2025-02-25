using Avalonia.ReactiveUI;

using PostSys.Gui.ViewModels;

namespace PostSys.Gui.Views.Controls;

public partial class ButtonsView : ReactiveUserControl<ButtonsViewModel>
{
	public ButtonsView()
	{
		InitializeComponent();
	}
}