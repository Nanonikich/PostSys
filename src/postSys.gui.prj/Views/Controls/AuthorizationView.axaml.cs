using Avalonia.ReactiveUI;
using PostSys.Gui.ViewModels;

namespace PostSys.Gui.Views.Controls;

public partial class AuthorizationView : ReactiveUserControl<AuthorizationViewModel>
{
	public AuthorizationView()
	{
		InitializeComponent();
	}
}