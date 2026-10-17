using System;
using Avalonia.Controls;
using Avalonia.Platform;

namespace PostSys.Gui.Common.Services.IconProvider;

/// <summary>Провайдер иконки приложения.</summary>
/// <param name="iconPath">Путь к иконке.</param>
public class ApplicationIconProvider(string iconPath) : IApplicationIconProvider
{
	/// <inheritdoc/>
	public WindowIcon Icon { get; } = new(AssetLoader.Open(new Uri(iconPath)));
}