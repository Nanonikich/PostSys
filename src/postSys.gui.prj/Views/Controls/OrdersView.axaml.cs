using ReactiveUI.Avalonia;

using PostSys.Gui.ViewModels;

namespace PostSys.Gui.Views.Controls;

public partial class OrdersView : ReactiveUserControl<OrdersViewModel>
{
	public OrdersView()
	{
		InitializeComponent();
	}
}