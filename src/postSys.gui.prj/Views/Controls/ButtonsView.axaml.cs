using ReactiveUI.Avalonia;
using PostSys.Gui.ViewModels;

namespace PostSys.Gui.Views.Controls;

/// <summary>Представление панели навигации.</summary>
public partial class ButtonsView : ReactiveUserControl<ButtonsViewModel>
{
    /// <summary>Создаёт представление панели навигации.</summary>
    public ButtonsView()
    {
        InitializeComponent();
    }
}