using System;

using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Controls.ApplicationLifetimes;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using ReactiveUI;

using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using Splat.NLog;

using PostSys.Client;
using PostSys.Client.Data;
using PostSys.Client.Data.SignalR;
using PostSys.Client.Data.Subscriptions;
using PostSys.Client.SignalR;
using PostSys.Client.Subscriptions;
using PostSys.Framework.Common;
using PostSys.Framework.Common.Logging;
using PostSys.Gui.Common.Services.IconProvider;
using PostSys.Gui.Configuration;
using PostSys.Gui.Helpers;
using PostSys.Gui.ViewModels;
using PostSys.Gui.Views.Windows;

namespace PostSys.Gui;

/// <summary>Настройки приложения.</summary>
public partial class App : Application
{
	public IServiceProvider Container { get; private set; }

	/// <inheritdoc/>
	public override void Initialize()
	{
		NLogConfigurationProvider.ConfigureNLog(ProfileLocationStorage.LogDirPath);
		Locator.CurrentMutable.UseNLogWithWrappingFullLogger();
		UnhandledExceptionsLogging.Subscribe();

		AvaloniaXamlLoader.Load(this);
	}

	/// <summary>Создаёт экземпляр класса <see cref="App"/>.</summary>
	public App()
	{
		Container = ConfigureServices();
	}

	private IServiceProvider ConfigureServices()
	{
		var host = Host
			.CreateDefaultBuilder()
			.ConfigureServices((context, services) =>
			{
				services.UseMicrosoftDependencyResolver();
				services
					.AddSingleton(sp => ApplicationConfiguration.Load(ProfileLocationStorage.ConfigPath))
					.AddSingleton<IApplicationIconProvider>(_ => new ApplicationIconProvider("avares://PostSys.Gui.Common/Assets/logo.ico"))
					.AddSingleton<PostmanHelper>()
					.AddSingleton<IClientGraphQlClient>(sp =>
					{
						var serviceAddress = sp.GetRequiredService<ApplicationConfiguration>().ServiceAddress;
						return new ClientGraphQlClient(serviceAddress);
					})
					.AddSingleton<IPostmanGraphQlClient>(sp =>
					{
						var serviceAddress = sp.GetRequiredService<ApplicationConfiguration>().ServiceAddress;
						return new PostmanGraphQlClient(serviceAddress);
					})
					.AddSingleton<IPackageGraphQlClient>(sp =>
					{
						var serviceAddress = sp.GetRequiredService<ApplicationConfiguration>().ServiceAddress;
						return new PackageGraphQlClient(serviceAddress);
					})
					.AddSingleton<IAddressClient>(sp =>
					{
						var apiKey = sp.GetRequiredService<ApplicationConfiguration>().YandexApiKey;
						return new AddressClient(apiKey);
					})
					.AddSingleton<ISignalRClient, SignalRClient>()
					.AddSingleton<IClientsMessageHandler, ClientsMessageHandler>()
					.AddSingleton<IPackagesMessageHandler, PackagesMessageHandler>()
					.AddSingleton<IPostmenMessageHandler, PostmenMessageHandler>()
					.AddSingleton<ISubscriptionClient>(sp =>
					{
						return new SubscriptionClient(
							sp.GetRequiredService<ApplicationConfiguration>().ServiceAddress,
							sp.GetRequiredService<IClientsMessageHandler>(),
							sp.GetRequiredService<IPostmenMessageHandler>(),
							sp.GetRequiredService<IPackagesMessageHandler>());
					})
					.AddTransient<AuthorizationViewModel>()
					.AddTransient<OrdersViewModel>()
					.AddTransient<CurrentOrderViewModel>()
					.AddTransient<ButtonsViewModel>()
					.AddTransient<OrderDescriptionViewModel>()
					.AddTransient<SettingsWindowViewModel>()
					.AddSingleton<InfoWindowViewModel>();

				var resolver = Locator.CurrentMutable;
				resolver.InitializeSplat();
				resolver.InitializeReactiveUI();

				RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;
				RxApp.DefaultExceptionHandler = new ExceptionHandler();
			})
			.Build();

		host.Start();
		return host.Services;
	}

	/// <summary>Инициализирует фреймворк.</summary>
	public override void OnFrameworkInitializationCompleted()
	{
		if(ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			desktop.Exit += OnApplicationStopping;

			desktop.MainWindow = new MainWindow
			{
				DataContext = new MainWindowViewModel(
					Container.GetRequiredService<ISignalRClient>(),
					Container.GetRequiredService<ISubscriptionClient>(),
					Container.GetRequiredService<IPostmenMessageHandler>(),
					Container.GetRequiredService<ApplicationConfiguration>().ServiceAddress,
					Container.GetRequiredService<PostmanHelper>(),
					Container.GetRequiredService<SettingsWindowViewModel>(),
					Container.GetRequiredService<InfoWindowViewModel>())
				{
					AuthorizationViewModel = Container.GetRequiredService<AuthorizationViewModel>(),
					OrdersViewModel = Container.GetRequiredService<OrdersViewModel>(),
					CurrentOrderViewModel = Container.GetRequiredService<CurrentOrderViewModel>(),
					ButtonsViewModel = Container.GetRequiredService<ButtonsViewModel>(),
				}
			};
		}

		base.OnFrameworkInitializationCompleted();
	}

	private void OnApplicationStopping(object? sender, ControlledApplicationLifetimeExitEventArgs e)
	{
		UnhandledExceptionsLogging.Unsubscribe();
	}
}

class ExceptionHandler : IObserver<Exception>
{
	public void OnCompleted() { }

	public void OnError(Exception error) { }

	public void OnNext(Exception value) => OnError(value);
}
