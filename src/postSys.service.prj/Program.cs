using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using PostSys.Application.Contracts;
using PostSys.Application.Extensions;
using PostSys.Dal.Extensions;
using PostSys.Framework.Common;
using PostSys.Framework.Common.Logging;
using PostSys.Service.Auth;
using PostSys.Service.Common.Contracts;
using PostSys.Service.Extensions;
using PostSys.Service.Outbox;
using PostSys.Service.SignalR;

var builder = WebApplication.CreateBuilder(args);

NLogConfigurationProvider.ConfigureNLog(ProfileLocationStorage.LogDirPath);
builder.Host.UseNLog();
UnhandledExceptionsLogging.Subscribe();

var configuration = builder.Configuration;
var databaseConnectionString = GetDatabaseConnectionString();
var services = builder.Services;

services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>() ?? new JwtOptions();

services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = jwtOptions.Issuer,
			ValidAudience = jwtOptions.Audience,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
			ClockSkew = TimeSpan.FromMinutes(1)
		};
	});
services.AddAuthorization();
services.AddSingleton<IJwtTokenService, JwtTokenService>();

services.AddSignalR();
services
	.AddHealthChecks()
	.AddNpgSql(databaseConnectionString, name: "postgres", tags: [ HealthChecksConstants.ReadinessTag ]);
services.AddPostSysObservability();

services.AddCqrs(typeof(ApplicationServiceCollectionExtensions).Assembly);
services.AddDatabase(databaseConnectionString);
services.AddGraphQl().AddAuthorization();
services.AddHostedService<OutboxPublisherHostedService>();

ConfigureCors(services, builder.Environment, configuration);

var app = builder.Build();

if(ShouldRunMigrations(configuration))
	await app.Services.MigrateDatabaseAsync();

RegisterServiceStatusBroadcast(app);

app.UseAuthentication();
app.UseAuthorization();
app.MapPostSysHealthChecks();
app.MapPostSysMetrics();
app.UseCors();
app.UseWebSockets();
app.MapGraphQL().WithOptions(options =>
{
	options.Tool.Enable = app.Environment.IsDevelopment();
});
app.MapHub<NotificationHub>("/notificationHub");
await app.RunAsync();

void ConfigureCors(IServiceCollection serviceCollection, IHostEnvironment environment, IConfiguration config)
{
	serviceCollection.AddCors(options =>
	{
		options.AddDefaultPolicy(policy =>
		{
			if(environment.IsDevelopment())
			{
				policy
					.AllowAnyMethod()
					.AllowAnyHeader()
					.SetIsOriginAllowed(_ => true)
					.AllowCredentials();
				return;
			}

			var origins = config.GetSection("Cors:Origins").Get<string[]>() ?? [];
			if(origins.Length == 0)
			{
				policy.SetIsOriginAllowed(_ => false);
				return;
			}

			policy
				.WithOrigins(origins)
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowCredentials();
		});
	});
}

string GetDatabaseConnectionString()
{
	var server = configuration["POSTGRES_HOST"] ?? "";
	var port = configuration["POSTGRES_PORT"] ?? "";
	var database = configuration["POSTGRES_DB"] ?? "";
	var user = configuration["POSTGRES_USER"] ?? "";
	var password = configuration["POSTGRES_PASSWORD"] ?? "";

	if(string.IsNullOrEmpty(server) || string.IsNullOrEmpty(port) || string.IsNullOrEmpty(database) ||
		string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
	{
		return configuration.GetConnectionString("PostgresConnection") ??
			throw new InvalidOperationException("Database connection string is not specified in the configuration.");
	}

	return $"Server={server};Port={port};Database={database};Username={user};Password={password}";
}

static bool ShouldRunMigrations(IConfiguration config)
{
	var raw = config["POSTSYS_RUN_MIGRATIONS"] ?? config["RunMigrations"];
	if(string.IsNullOrWhiteSpace(raw))
		return true;

	return !string.Equals(raw, "0", StringComparison.OrdinalIgnoreCase)
		&& !string.Equals(raw, "false", StringComparison.OrdinalIgnoreCase)
		&& !string.Equals(raw, "no", StringComparison.OrdinalIgnoreCase);
}

static void RegisterServiceStatusBroadcast(WebApplication application)
{
	var hubContext = application.Services.GetRequiredService<IHubContext<NotificationHub>>();
	var lifetime = application.Services.GetRequiredService<IHostApplicationLifetime>();
	var logger = application.Services.GetRequiredService<ILoggerFactory>().CreateLogger("ServiceStatusBroadcast");

	lifetime.ApplicationStarted.Register(() =>
	{
		_ = BroadcastStatusAsync(hubContext, logger, isAvailable: true, lifetime.ApplicationStopping);
	});
	lifetime.ApplicationStopping.Register(() =>
	{
		_ = BroadcastStatusAsync(hubContext, logger, isAvailable: false, CancellationToken.None);
	});
}

static async Task BroadcastStatusAsync(
	IHubContext<NotificationHub> hubContext,
	ILogger logger,
	bool isAvailable,
	CancellationToken cancellationToken)
{
	try
	{
		await hubContext.Clients.All.SendAsync("ReceiveServiceStatus", isAvailable, cancellationToken);
	}
	catch(OperationCanceledException)
	{
	}
	catch(Exception ex)
	{
		logger.LogDebug(ex, "Не удалось отправить состояние службы клиентам.");
	}
}
