using Android.App;
using Android.Content.PM;

using Avalonia.Android;

namespace PostSys.Gui.Android;

[Activity(
	Label = "PostSys",
	Theme = "@style/MyTheme.NoActionBar",
	Icon = "@drawable/icon",
	MainLauncher = true,
	ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode | ConfigChanges.ScreenLayout)]
public class MainActivity : AvaloniaMainActivity;
