using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

using Avalonia;
using Avalonia.Android;

using PostSys.Gui;

namespace PostSys.Gui.Android;

[Activity(
	Label = "PostSys",
	Theme = "@style/MyTheme.NoActionBar",
	Icon = "@drawable/icon",
	MainLauncher = true,
	LaunchMode = LaunchMode.SingleTop,
	ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode | ConfigChanges.ScreenLayout)]
[IntentFilter(
	[Intent.ActionView],
	Categories = [Intent.CategoryDefault, Intent.CategoryBrowsable],
	DataScheme = "postsys",
	DataHost = "configure")]
public class MainActivity : AvaloniaMainActivity
{
	protected override void OnCreate(Bundle? savedInstanceState)
	{
		base.OnCreate(savedInstanceState);
		TryHandleIntent(Intent);
	}

	protected override void OnNewIntent(Intent? intent)
	{
		base.OnNewIntent(intent);
		Intent = intent;
		TryHandleIntent(intent);
	}

	private static void TryHandleIntent(Intent? intent)
	{
		var data = intent?.DataString;
		if(string.IsNullOrWhiteSpace(data) || !Uri.TryCreate(data, UriKind.Absolute, out var uri))
			return;

		if(Avalonia.Application.Current is App app)
			app.HandleDeepLink(uri);
	}
}
