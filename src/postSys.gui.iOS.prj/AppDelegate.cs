using Foundation;

using Avalonia;
using Avalonia.iOS;

using ReactiveUI.Avalonia;

using PostSys.Gui;

namespace PostSys.Gui.iOS;

[Register("AppDelegate")]
#pragma warning disable CA1711
public partial class AppDelegate : AvaloniaAppDelegate<App>
#pragma warning restore CA1711
{
	protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
	{
		return base.CustomizeAppBuilder(builder)
			.UseReactiveUI(rxui => rxui.WithExceptionHandler(new ExceptionHandler()))
			.WithInterFont()
			.LogToTrace();
	}
}
