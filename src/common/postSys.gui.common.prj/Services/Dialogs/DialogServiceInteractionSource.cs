using System;
using System.Collections.Generic;

using Avalonia.Controls;

using Splat;

using PostSys.Gui.Common.Services.IconProvider;
using System.Net.Http;

namespace PostSys.Gui.Common.Services.Dialogs;

/// <summary>Создаёт взаимодействия открытия для окон.</summary>
/// <typeparam name="TViewModel">Модель представления окна для взаимодействий.</typeparam>
public class DialogServiceInteractionSource<TViewModel> : IDisposable
	where TViewModel : Window
{
	#region Data

	private List<IDisposable> _disposeInteractions;
	private readonly DialogReactiveWindow _dialogReactiveWindow;

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="DialogServiceInteractionSource{TViewModel}"/>.</summary>
	/// <param name="viewModel">Окно для построения взаимодействий.</param>
	public DialogServiceInteractionSource(TViewModel viewModel)
	{
		_dialogReactiveWindow = new(viewModel)
		{
			IconProvider = Locator.Current.GetService<IApplicationIconProvider>()!,
		};
		_disposeInteractions = [];
	}

	#endregion

	#region Methods

	/// <summary>Регистрация обработчиков взаимодействий.</summary>
	/// <param name="dialogServiceInteractions">Сервис взаимодействий.</param>
	public void Register(DialogServiceInteractions dialogServiceInteractions)
	{
		_disposeInteractions =
		[
			dialogServiceInteractions.ShowQuestionMessage.RegisterHandler(_dialogReactiveWindow.ShowQuestionMessageAsync),
			dialogServiceInteractions.ShowErrorMessage.RegisterHandler(_dialogReactiveWindow.ShowErrorMessageAsync),
			dialogServiceInteractions.CloseWindow.RegisterHandler(_dialogReactiveWindow.CloseWindow)
		];
	}

	#endregion

	#region IDisposable

	/// <summary>Возвращает или устанавливает флаг уничтожены ли зависимости.</summary>
	/// <value>Флаг уничтожены ли зависимости.</value>
	public bool IsDisposed { get; private set; }

	/// <inheritdoc/>
	public void Dispose()
	{
		if(!IsDisposed)
		{
			foreach(var interaction in _disposeInteractions)
			{
				interaction.Dispose();
			}
			_disposeInteractions.Clear();
			IsDisposed = true;
		}
	}

	#endregion
}