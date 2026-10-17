using Avalonia.Controls;

namespace PostSys.Gui.Common.Services.IconProvider;

/// <summary>Иконка для окон.</summary>
public interface IApplicationIconProvider
{
	/// <summary>Возвращает иконку приложения.</summary>
	/// <value>Иконка приложения.</value>
	WindowIcon Icon { get; }
}