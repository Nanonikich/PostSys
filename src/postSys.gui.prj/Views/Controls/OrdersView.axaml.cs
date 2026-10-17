using ReactiveUI.Avalonia;
using PostSys.Gui.ViewModels;

namespace PostSys.Gui.Views.Controls;

/// <summary>Представление списка заказов.</summary>
public partial class OrdersView : ReactiveUserControl<OrdersViewModel>
{
	/// <summary>Создаёт представление списка заказов.</summary>
	public OrdersView()
	{
		InitializeComponent();
	}
}