using System.Reactive.Disposables;

using ReactiveUI;
using ReactiveUI.Avalonia;

using PostSys.Gui.Common.Services.Dialogs;
using PostSys.Gui.ViewModels;

namespace PostSys.Gui.Views.Windows;

public partial class SettingsWindow : ReactiveWindow<SettingsWindowViewModel>
{
	public SettingsWindow()
	{
		InitializeComponent();

		this.WhenActivated(d =>
		{
			var dialogHost = new DialogReactiveWindow(this);
			var interactions = new DialogServiceInteractionSource(dialogHost);
			interactions.Register(ViewModel!.DialogServiceInteractions);

			d(Disposable.Create(interactions.Dispose));
		});
	}
}
