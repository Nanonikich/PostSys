using System;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SignalR;

using NLog.Web;

using PostSys.Dal.Extensions;
using PostSys.Dal.ReadModel;
using PostSys.Framework.Common;
using PostSys.Framework.Common.Logging;
using PostSys.Service.Common.Contracts;
using PostSys.Service.Extensions;
using PostSys.Service.SignalR;

var builder = WebApplication.CreateBuilder(args);

NLogConfigurationProvider.ConfigureNLog(ProfileLocationStorage.LogDirPath);
builder.Host.UseNLog();
UnhandledExceptionsLogging.Subscribe();

var configuration = builder.Configuration;

var databaseConnectionString = GetDatabaseConnectionString();
var services = builder.Services;

services.AddSignalR();
services.AddHttpClient();
services
	.AddHealthChecks()
	.AddNpgSql(databaseConnectionString, tags: [ HealthChecksConstants.ReadinessTag ]);

services.AddCqrs(assemblies: Assembly.GetExecutingAssembly());
services.AddDatabase(databaseConnectionString);
services.AddGraphQl<ReadDbContext>();

ConfigureCors(services);

var app = builder.Build();

var hubContext = app.Services.GetRequiredService<IHubContext<NotificationHub>>();
var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
lifetime.ApplicationStarted.Register(() => Task.Run(async () => await hubContext.Clients.All.SendAsync("ReceiveServiceStatus", true)));
lifetime.ApplicationStopping.Register(() => Task.Run(async() => await hubContext.Clients.All.SendAsync("ReceiveServiceStatus", false)));

app.MapHealthChecks("/health");
app.UseCors();
app.MapGraphQL();
app.MapHub<NotificationHub>("/notificationHub");
app.UseRouting();
app.Run();


void ConfigureCors(IServiceCollection services)
{
	services.AddCors(options =>
	{
		options.AddDefaultPolicy(builder =>
		{
			builder
				.AllowAnyMethod()
				.AllowAnyHeader()
				.SetIsOriginAllowed(_ => true)
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