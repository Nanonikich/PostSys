using System;

using NLog;

using ReactiveUI;

using PostSys.Client.Data;
using PostSys.Framework.Common;
using PostSys.Gui.Common.ViewModels;
using PostSys.Gui.Localization;

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления экрана «О программе».</summary>
public class InfoWindowViewModel : ViewModelBase
{
	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	private readonly IInfoGraphQlClient _infoGraphQlClient;
	private string? _cachedApiVersion;
	private bool _apiVersionLoaded;
	private string _versionGui = string.Empty;
	private string _versionApi = string.Empty;

	/// <summary>Строка версии приложения.</summary>
	public string VersionGui
	{
		get => _versionGui;
		set => this.RaiseAndSetIfChanged(ref _versionGui, value);
	}

	/// <summary>Строка версии службы.</summary>
	public string VersionApi
	{
		get => _versionApi;
		set => this.RaiseAndSetIfChanged(ref _versionApi, value);
	}

	/// <summary>Создаёт экземпляр класса <see cref="InfoWindowViewModel"/>.</summary>
	/// <param name="infoGraphQlClient">Клиент сведений о службе.</param>
	public InfoWindowViewModel(IInfoGraphQlClient infoGraphQlClient)
	{
		_infoGraphQlClient = infoGraphQlClient;
		RefreshLocalizedTexts();
		InitializeAsync();
	}

	/// <summary>Обновляет локализованные строки после смены языка.</summary>
	public void RefreshLocalizedTexts()
	{
		var version = AssemblyInfo.FromEntryAssembly().Version;
		VersionGui = UiLocalizer.Format(
			"Info_VersionGui",
			$"{version!.Major}.{version.Minor}.{version.Build}");

		if(!_apiVersionLoaded)
			return;

		VersionApi = string.IsNullOrWhiteSpace(_cachedApiVersion)
			? UiLocalizer.Get("Info_VersionApiUnavailable")
			: UiLocalizer.Format("Info_VersionApi", _cachedApiVersion);
	}

	private async void InitializeAsync()
	{
		try
		{
			_cachedApiVersion = await _infoGraphQlClient.GetVersionAsync();
			_apiVersionLoaded = true;
			VersionApi = string.IsNullOrWhiteSpace(_cachedApiVersion)
				? UiLocalizer.Get("Info_VersionApiUnavailable")
				: UiLocalizer.Format("Info_VersionApi", _cachedApiVersion);
		}
		catch(Exception ex)
		{
			_apiVersionLoaded = true;
			_cachedApiVersion = null;
			VersionApi = UiLocalizer.Get("Info_VersionApiError");
			Log.Error(ex, "Error while fetching version.");
		}
	}
}
