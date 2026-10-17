using System.Reactive.Disposables;

using ReactiveUI.Avalonia;

using ReactiveUI;

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
			var iteractionImpl = new DialogServiceInteractionSource<SettingsWindow>(this);
			iteractionImpl.Register(ViewModel!.DialogServiceInteractions);

			d(Disposable.Create(() => iteractionImpl.Dispose()));
		});
	}
}
