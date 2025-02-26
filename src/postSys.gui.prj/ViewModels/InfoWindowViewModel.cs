using System;

using NLog;

using ReactiveUI;

using PostSys.Client.Data;
using PostSys.Framework.Common;
using PostSys.Gui.Common.ViewModels;

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления окна "О программе".</summary>
public class InfoWindowViewModel : ViewModelBase
{
	#region Statics

	private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

	#endregion

	#region Data

	private string _versionGui;
	private string _versionApi;

	#endregion

	#region Properties

	/// <summary>Возвращает или задаёт версию приложения.</summary>
	/// <value>Версия приложения.</value>
	public string VersionGui
	{
		get => _versionGui;
		set => this.RaiseAndSetIfChanged(ref _versionGui, value);
	}

	/// <summary>Возвращает или задаёт версию сервиса.</summary>
	/// <value>Версия сервиса.</value>
	public string VersionApi
	{
		get => _versionApi;
		set => this.RaiseAndSetIfChanged(ref _versionApi, value);
	}

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="InfoWindowViewModel"/>.</summary>
	/// <param name="infoGraphQlClient"><see cref="IInfoGraphQlClient"/>.</param>
	public InfoWindowViewModel(IInfoGraphQlClient infoGraphQlClient)
	{
		var version = AssemblyInfo.FromEntryAssembly().Version;
		VersionGui = $"Version GUI: {version!.Major}.{version!.Minor}.{version!.Build}";

		InitializeAsync(infoGraphQlClient);
	}

	#endregion

	#region Methods

	private async void InitializeAsync(IInfoGraphQlClient infoGraphQlClient)
	{
		try
		{
			var versionApi = await infoGraphQlClient.GetVersionAsync();
			VersionApi = string.IsNullOrWhiteSpace(versionApi)
				? "Version API: Cannot get version"
				: $"Version API: {versionApi}";
		}
		catch(Exception ex)
		{
			VersionApi = "Version API: Error while fetching version";
			Log.Error(ex, "Error while fetching version.");
		}
	}

	#endregion
}
