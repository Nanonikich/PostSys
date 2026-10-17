using System;
using System.Collections.Generic;

using Splat;

using PostSys.Gui.Common.Services.IconProvider;

namespace PostSys.Gui.Common.Services.Dialogs;

/// <summary>Регистрирует взаимодействия диалогов для текущего host UI.</summary>
public sealed class DialogServiceInteractionSource : IDisposable
{
	private readonly List<IDisposable> _disposeInteractions = [];
	private readonly DialogReactiveWindow _dialogReactiveWindow;
	private bool _isDisposed;

	/// <summary>Создаёт регистратор взаимодействий.</summary>
	public DialogServiceInteractionSource(DialogReactiveWindow dialogReactiveWindow)
	{
		_dialogReactiveWindow = dialogReactiveWindow ?? throw new ArgumentNullException(nameof(dialogReactiveWindow));
		_dialogReactiveWindow.IconProvider = Locator.Current.GetService<IApplicationIconProvider>()!;
	}

	/// <summary>Регистрация обработчиков взаимодействий.</summary>
	public void Register(DialogServiceInteractions dialogServiceInteractions)
	{
		_disposeInteractions.Add(dialogServiceInteractions.ShowQuestionMessage.RegisterHandler(_dialogReactiveWindow.ShowQuestionMessageAsync));
		_disposeInteractions.Add(dialogServiceInteractions.ShowErrorMessage.RegisterHandler(_dialogReactiveWindow.ShowErrorMessageAsync));
		_disposeInteractions.Add(dialogServiceInteractions.CloseWindow.RegisterHandler(_dialogReactiveWindow.CloseWindow));
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		if(_isDisposed)
			return;

		foreach(var interaction in _disposeInteractions)
			interaction.Dispose();

		_disposeInteractions.Clear();
		_isDisposed = true;
		GC.SuppressFinalize(this);
	}
}
