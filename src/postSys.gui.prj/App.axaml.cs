using System;
using System.Net.Http;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using Splat.NLog;
using PostSys.Client;
using PostSys.Client.Data;
using PostSys.Client.Data.SignalR;
using PostSys.Client.Data.Subscriptions;
using PostSys.Client.GraphQl;
using PostSys.Client.SignalR;
using PostSys.Client.Subscriptions;
using PostSys.Framework.Common;
using PostSys.Framework.Common.Logging;
using PostSys.Gui.Common.Services.IconProvider;
using PostSys.Gui.Configuration;
using PostSys.Gui.Helpers;
using PostSys.Gui.Localization;
using PostSys.Gui.Services.Storage;
using PostSys.Gui.ViewModels;
using PostSys.Gui.Views.Controls;

namespace PostSys.Gui;

/// <summary>Корневой объект мобильного приложения (Android).</summary>
public partial class App : Application
{
    private IHost? _host;
    private bool _isShuttingDown;

    /// <summary>Контейнер внедрения зависимостей.</summary>
    public IServiceProvider Container { get; private set; } = null!;

    /// <summary>Текущая главная модель представления (для внешней навигации).</summary>
    public MainWindowViewModel? MainViewModel { get; private set; }

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
        _host = CreateHost();
        Container = _host.Services;
        var configuration = Container.GetRequiredService<ApplicationConfiguration>();
        UiLocalizer.ApplyCulture(configuration.UiLanguage);
        _host.Start();
    }

    private static IHost CreateHost()
    {
        return Host
            .CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.UseMicrosoftDependencyResolver();

                services
                    .AddSingleton(sp => ApplicationConfiguration.Load(ProfileLocationStorage.ConfigPath))
                    .AddSingleton<IApplicationIconProvider>(_ => new ApplicationIconProvider("avares://postSys.gui/Assets/logo.ico"))
                    .AddSingleton<PostmanHelper>()
                    .AddSingleton<ISecureSessionStore, AesSecureSessionStore>()
                    .AddSingleton<IOrdersCache, FileOrdersCache>();

                services
                    .AddHttpClient("postsys", (sp, client) =>
                    {
                        var address = sp.GetRequiredService<ApplicationConfiguration>().ServiceAddress.TrimEnd('/');
                        client.BaseAddress = new Uri(address);
                        client.Timeout = TimeSpan.FromSeconds(30);
                        client.DefaultRequestVersion = System.Net.HttpVersion.Version20;
                        client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower;
                    })
                    .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
                    {
                        PooledConnectionLifetime = TimeSpan.FromMinutes(5),
                        PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2),
                        EnableMultipleHttp2Connections = true
                    })
                    .AddStandardResilienceHandler();

                services
                    .AddHttpClient("yandex-geocode", client =>
                    {
                        client.BaseAddress = new Uri("https://geocode-maps.yandex.ru/");
                        client.Timeout = TimeSpan.FromSeconds(15);
                    })
                    .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
                    {
                        PooledConnectionLifetime = TimeSpan.FromMinutes(5),
                        PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2)
                    })
                    .AddStandardResilienceHandler();

                services.AddSingleton(sp =>
                {
                    var config = sp.GetRequiredService<ApplicationConfiguration>();
                    var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("postsys");
                    return new GraphQlConnection(config.ServiceAddress, httpClient);
                });

                services
                    .AddSingleton<IClientGraphQlClient, ClientGraphQlClient>()
                    .AddSingleton<IPostmanGraphQlClient, PostmanGraphQlClient>()
                    .AddSingleton<IPackageGraphQlClient, PackageGraphQlClient>()
                    .AddSingleton<IInfoGraphQlClient, InfoGraphQlClient>()
                    .AddSingleton<IAddressClient>(sp =>
                    {
                        var apiKey = sp.GetRequiredService<ApplicationConfiguration>().YandexApiKey;
                        var http = sp.GetRequiredService<IHttpClientFactory>().CreateClient("yandex-geocode");
                        return new AddressClient(http, apiKey);
                    })
                    .AddSingleton<ISignalRClient, SignalRClient>()
                    .AddSingleton<IClientsMessageHandler, ClientsMessageHandler>()
                    .AddSingleton<IPackagesMessageHandler, PackagesMessageHandler>()
                    .AddSingleton<IPostmenMessageHandler, PostmenMessageHandler>()
                    .AddSingleton<ISubscriptionClient, SubscriptionClient>()
                    .AddTransient<AuthorizationViewModel>()
                    .AddTransient<OrdersViewModel>()
                    .AddTransient<CurrentOrderViewModel>()
                    .AddTransient<ButtonsViewModel>()
                    .AddTransient<OrderDescriptionViewModel>()
                    .AddTransient<SettingsWindowViewModel>()
                    .AddSingleton<InfoWindowViewModel>()
                    .AddTransient<MainWindowViewModel>();
            })
            .Build();
    }

    private MainWindowViewModel CreateMainViewModel()
    {
        var vm = Container.GetRequiredService<MainWindowViewModel>();
        vm.AuthorizationViewModel = Container.GetRequiredService<AuthorizationViewModel>();
        vm.OrdersViewModel = Container.GetRequiredService<OrdersViewModel>();
        vm.CurrentOrderViewModel = Container.GetRequiredService<CurrentOrderViewModel>();
        vm.ButtonsViewModel = Container.GetRequiredService<ButtonsViewModel>();
        MainViewModel = vm;
        return vm;
    }

    /// <summary>Обрабатывает внешнюю ссылку настройки адреса службы (например, postsys://configure?service=http://host:5000).</summary>
    /// <param name="uri">Адрес внешней ссылки приложения.</param>
    public void HandleDeepLink(Uri uri)
    {
        if(!string.Equals(uri.Scheme, "postsys", StringComparison.OrdinalIgnoreCase))
            return;

        var service = GetQueryParam(uri, "service") ?? GetQueryParam(uri, "address");
        if(!string.IsNullOrWhiteSpace(service))
            MainViewModel?.ApplyDeepLinkServiceAddress(service);
    }

    private static string? GetQueryParam(Uri uri, string key)
    {
        var query = uri.Query.TrimStart('?');
        foreach(var part in query.Split('&', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var pair = part.Split('=', 2);
            if(pair.Length != 2)
                continue;
            if(string.Equals(Uri.UnescapeDataString(pair[0]), key, StringComparison.OrdinalIgnoreCase))
                return Uri.UnescapeDataString(pair[1]);
        }

        return null;
    }

    /// <inheritdoc/>
    public override void OnFrameworkInitializationCompleted()
    {
        if(ApplicationLifetime is IControlledApplicationLifetime controlled)
            controlled.Exit += OnLifetimeExit;

        if(ApplicationLifetime is IActivityApplicationLifetime activityLifetime)
        {
            activityLifetime.MainViewFactory = () => new MainView { DataContext = CreateMainViewModel() };
        }
        else if(ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = CreateMainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

	private void OnLifetimeExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
	{
		try
		{
			ShutdownAsync().GetAwaiter().GetResult();
		}
		catch(Exception ex)
		{
			System.Diagnostics.Debug.WriteLine(ex);
		}
	}

	private async System.Threading.Tasks.Task ShutdownAsync()
	{
		if(_isShuttingDown)
			return;

		_isShuttingDown = true;

		try
		{
			if(MainViewModel is IDisposable disposableMain)
				disposableMain.Dispose();
			MainViewModel = null;

			if(Container.GetService<ISubscriptionClient>() is { } subscriptions)
				await subscriptions.DisposeAsync().ConfigureAwait(false);

			if(Container.GetService<ISignalRClient>() is { } signalR)
				await signalR.DisposeAsync().ConfigureAwait(false);

			if(Container.GetService<GraphQlConnection>() is { } connection)
				await connection.DisposeAsync().ConfigureAwait(false);
		}
		finally
		{
			UnhandledExceptionsLogging.Unsubscribe();
			if(_host is not null)
			{
				await _host.StopAsync(TimeSpan.FromSeconds(2)).ConfigureAwait(false);
				_host.Dispose();
				_host = null;
			}
		}
	}
}

/// <summary>Глобальный обработчик необработанных исключений ReactiveUI.</summary>
public sealed class ExceptionHandler : IObserver<Exception>
{
    /// <inheritdoc/>
    public void OnCompleted() { }

    /// <summary>Обрабатывает критическую ошибку (заглушка для подавления падения UI).</summary>
    /// <param name="error">Исключение.</param>
    public void OnError(Exception error) { }

    /// <summary>Передаёт следующее исключение в <see cref="OnError"/>.</summary>
    /// <param name="value">Исключение из потока ReactiveUI.</param>
    public void OnNext(Exception value) => OnError(value);
}