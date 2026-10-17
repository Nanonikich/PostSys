using System;
using System.Collections.Generic;
using System.Reactive.Disposables;

using ReactiveUI.Avalonia;

using ReactiveUI;

using PostSys.Gui.Common.Services.Dialogs;
using PostSys.Gui.ViewModels;

namespace PostSys.Gui.Views.Windows;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
	#region Data

	private DialogReactiveWindow _dialogReactiveWindow = null!;
	private List<IDisposable> _interactionsDispose = null!;

	#endregion

	#region .ctor

	public MainWindow()
	{
		InitializeComponent();

		_dialogReactiveWindow = new(this);

		this.WhenActivated(d =>
		{
			var iteractionImpl = new DialogServiceInteractionSource<MainWindow>(this);
			iteractionImpl.Register(ViewModel!.DialogServiceInteractions);
			iteractionImpl.Register(ViewModel!.AuthorizationViewModel.DialogServiceInteractions);
			iteractionImpl.Register(ViewModel!.OrdersViewModel.DialogServiceInteractions);
			iteractionImpl.Register(ViewModel!.CurrentOrderViewModel.DialogServiceInteractions);

			_interactionsDispose =
			[
				.. new List<IDisposable>()
				{
					ViewModel!.ShowSettingsWindow
						.RegisterHandler(_dialogReactiveWindow.ShowDialogWindowAsync<SettingsWindowViewModel, SettingsWindow>),
					ViewModel!.ShowInfoWindow
						.RegisterHandler(_dialogReactiveWindow.ShowDialogWindowAsync<InfoWindowViewModel, InfoWindow>)
				},
			];

			d(Disposable.Create(() =>
			{
				iteractionImpl.Dispose();
				_interactionsDispose.ForEach(x => x.Dispose());
				_interactionsDispose.Clear();
			}));
		});
	}

	#endregion
}
