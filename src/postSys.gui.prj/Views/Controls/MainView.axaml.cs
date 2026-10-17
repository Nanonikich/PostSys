using System;
using System.Collections.Generic;
using System.Reactive.Disposables;

using Avalonia.Controls;

using ReactiveUI;
using ReactiveUI.Avalonia;

using PostSys.Gui.Common.Services.Dialogs;
using PostSys.Gui.ViewModels;
using PostSys.Gui.Views.Windows;

namespace PostSys.Gui.Views.Controls;

/// <summary>Главное представление мобильного приложения.</summary>
public partial class MainView : ReactiveUserControl<MainWindowViewModel>
{
	public MainView()
	{
		InitializeComponent();

		this.WhenActivated(d =>
		{
			if(ViewModel is null)
				return;

			var topLevel = TopLevel.GetTopLevel(this)
				?? throw new InvalidOperationException("TopLevel is not available.");

			var dialogHost = new DialogReactiveWindow(
				topLevel,
				ViewModel.ShowOverlay,
				ViewModel.ClearOverlay);
			var interactionSource = new DialogServiceInteractionSource(dialogHost);
			interactionSource.Register(ViewModel.DialogServiceInteractions);
			interactionSource.Register(ViewModel.AuthorizationViewModel.DialogServiceInteractions);
			interactionSource.Register(ViewModel.OrdersViewModel.DialogServiceInteractions);
			interactionSource.Register(ViewModel.CurrentOrderViewModel.DialogServiceInteractions);

			// На mobile Settings показывается overlay'ем — CloseWindow должен сбрасывать OverlayView.
			if(topLevel is not Window)
				interactionSource.Register(ViewModel.SettingsWindowViewModel.DialogServiceInteractions);

			List<IDisposable> dialogHandlers =
			[
				ViewModel.ShowSettingsWindow.RegisterHandler(dialogHost.ShowDialogOrOverlayAsync<SettingsWindowViewModel, SettingsWindow>),
				ViewModel.ShowInfoWindow.RegisterHandler(dialogHost.ShowDialogOrOverlayAsync<InfoWindowViewModel, InfoWindow>)
			];

			d(Disposable.Create(() =>
			{
				interactionSource.Dispose();
				foreach(var handler in dialogHandlers)
					handler.Dispose();
			}));
		});
	}
}
