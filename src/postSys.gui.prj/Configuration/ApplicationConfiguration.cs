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
	#region Properties

	/// <summary>Возвращает и задает адрес подключения к сервису.</summary>
	/// <value>Адрес подключения к сервису.</value>
	[JsonPropertyName("service_address")]
	public string ServiceAddress { get; set; } = "http://localhost:5000";
	
	/// <summary>Возвращает и задает ключ Яндекс.API.</summary>
	/// <value>Ключ Яндекс.API.</value>
	[JsonPropertyName("yandex_api_key")]
	public Guid YandexApiKey { get; set; } = Guid.Parse("abdd66b3-331d-4c4b-910d-4e6830274ddc");

	#endregion

	#region Methods

	/// <summary>Загружает конфигурацию приложения.</summary>
	/// <param name="path">Путь к файлу конфигурации.</param>
	/// <returns>Конфигурация приложения.</returns>
	public static ApplicationConfiguration Load(string path)
	{
		try
		{
			using var json = new FileStream(path, FileMode.OpenOrCreate);

			return JsonSerializer.Deserialize<ApplicationConfiguration>(json);
		}
		catch(Exception)
		{
			throw;
		}
	}

	/// <summary>Сохраняет конфигурацию по пути.</summary>
	/// <param name="path">Путь, куда сохранится конфигурация.</param>
	public void Save(string path)
	{
		var json = JsonSerializer.Serialize(this, new JsonSerializerOptions
		{
			WriteIndented = true
		});
		File.WriteAllText(path, json);
	}

	#endregion
}