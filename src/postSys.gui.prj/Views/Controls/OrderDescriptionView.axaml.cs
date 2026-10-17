using ReactiveUI.Avalonia;

using PostSys.Gui.ViewModels;

namespace PostSys.Gui.Views.Controls;

public partial class OrderDescriptionView : ReactiveUserControl<OrderDescriptionViewModel>
{
	public OrderDescriptionView()
	{
		InitializeComponent();
	}
}