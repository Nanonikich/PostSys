using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using Avalonia.Controls;
using ReactiveUI;
using ReactiveUI.Avalonia;
using PostSys.Gui.Common.Services.Dialogs;
using PostSys.Gui.ViewModels;

namespace PostSys.Gui.Views.Controls;

/// <summary>Главное представление мобильного приложения.</summary>
public partial class MainView : ReactiveUserControl<MainWindowViewModel>
{
    /// <summary>Создаёт главное представление и регистрирует обработчики диалогов.</summary>
    public MainView()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            if(ViewModel is null)
                return;

            var topLevel = TopLevel.GetTopLevel(this)
                ?? throw new InvalidOperationException("TopLevel is not available.");

			var dialogHost = new DialogReactiveWindow(topLevel, ViewModel.ClearOverlay);
            var interactionSource = new DialogServiceInteractionSource(dialogHost);
            interactionSource.Register(ViewModel.DialogServiceInteractions);
            interactionSource.Register(ViewModel.AuthorizationViewModel.DialogServiceInteractions);
            interactionSource.Register(ViewModel.OrdersViewModel.DialogServiceInteractions);
            interactionSource.Register(ViewModel.CurrentOrderViewModel.DialogServiceInteractions);
            interactionSource.Register(ViewModel.SettingsWindowViewModel.DialogServiceInteractions);

            List<IDisposable> dialogHandlers =
            [
                ViewModel.ShowSettingsWindow.RegisterHandler(ctx =>
                {
                    ViewModel.ShowOverlay(ctx.Input);
                    ctx.SetOutput(Unit.Default);
                    return System.Threading.Tasks.Task.CompletedTask;
                }),
                ViewModel.ShowInfoWindow.RegisterHandler(ctx =>
                {
                    ViewModel.ShowOverlay(ctx.Input);
                    ctx.SetOutput(Unit.Default);
                    return System.Threading.Tasks.Task.CompletedTask;
                })
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