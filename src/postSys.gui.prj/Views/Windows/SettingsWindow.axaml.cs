using System.Reactive.Disposables;

using Avalonia.ReactiveUI;

using ReactiveUI;

using PostSys.Gui.Common.Services.Dialogs;
using PostSys.Gui.ViewModels;

namespace PostSys.Gui.Views.Windows;

public partial class SettingsWindow : ReactiveWindow<SettingsWindowViewModel>
{
	public SettingsWindow()
	{
		InitializeComponent();

		this.WhenActivated(_ =>
		{
			var iteractionImpl = new DialogServiceInteractionSource<SettingsWindow>(this);
			iteractionImpl.Register(ViewModel!.DialogServiceInteractions);

			Disposable
				.Create(() => iteractionImpl.Dispose())
				.DisposeWith(_);
		});
	}
}