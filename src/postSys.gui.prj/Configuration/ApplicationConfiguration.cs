using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PostSys.Gui.Configuration;

/// <summary>Датаконтракт, содержащий настройки приложения.</summary>
[DataContract]
public record ApplicationConfiguration
{
	/// <summary>Адрес подключения к сервису.</summary>
	[JsonPropertyName("service_address")]
	public string ServiceAddress { get; set; } = DefaultServiceAddress();

	/// <summary>Ключ Яндекс.API.</summary>
	[JsonPropertyName("yandex_api_key")]
	public Guid YandexApiKey { get; set; } = Guid.Parse("abdd66b3-331d-4c4b-910d-4e6830274ddc");

	/// <summary>Загружает конфигурацию приложения.</summary>
	public static ApplicationConfiguration Load(string path)
	{
		try
		{
			var directory = Path.GetDirectoryName(path);
			if(!string.IsNullOrWhiteSpace(directory))
				Directory.CreateDirectory(directory);

			if(!File.Exists(path) || new FileInfo(path).Length == 0)
			{
				var defaults = new ApplicationConfiguration();
				defaults.Save(path);
				return defaults;
			}

			using var json = File.OpenRead(path);
			return JsonSerializer.Deserialize<ApplicationConfiguration>(json)
				?? new ApplicationConfiguration();
		}
		catch
		{
			return new ApplicationConfiguration();
		}
	}

	/// <summary>Сохраняет конфигурацию по пути.</summary>
	public void Save(string path)
	{
		try
		{
			var directory = Path.GetDirectoryName(path);
			if(!string.IsNullOrWhiteSpace(directory))
				Directory.CreateDirectory(directory);

			var json = JsonSerializer.Serialize(this, new JsonSerializerOptions
			{
				WriteIndented = true
			});
			File.WriteAllText(path, json);
		}
		catch
		{
			// Mobile sandbox / read-only FS — не роняем приложение.
		}
	}

	private static string DefaultServiceAddress()
	{
		// Android emulator: host loopback.
		if(OperatingSystem.IsAndroid())
			return "http://10.0.2.2:5000";

		return "http://localhost:5000";
	}
}
