using ReactiveUI.Avalonia;
using PostSys.Gui.ViewModels;

namespace PostSys.Gui.Views.Controls;

/// <summary>Представление карточки заказа.</summary>
public partial class OrderDescriptionView : ReactiveUserControl<OrderDescriptionViewModel>
{
    /// <summary>Создаёт представление карточки заказа.</summary>
    public OrderDescriptionView()
    {
        InitializeComponent();
    }
}