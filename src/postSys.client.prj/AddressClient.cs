using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using PostSys.Client.Data;

namespace PostSys.Client;

/// <summary>Клиент получения адреса через Яндекс.API по координатам.</summary>
public class AddressClient : IAddressClient
{
	#region Data

	private readonly Guid _apiKey;

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="AddressClient"/>.</summary>
	/// <param name="apiKey">Ключ к Яндекс.API.</param>
	public AddressClient(Guid apiKey)
	{
		_apiKey = apiKey;
	}

	#endregion

	#region Methods

	/// <inheritdoc/>
	public async Task<string> GetAddressAsync(double latitude, double longitude, CancellationToken cancellationToken = default)
	{
		using var client = new HttpClient();
	
		var url = $"https://geocode-maps.yandex.ru/1.x/?apikey={_apiKey}&geocode={longitude},{latitude}&format=json&lang=en-US";
		var response = await client.GetAsync(url, cancellationToken);

		if(response.IsSuccessStatusCode)
		{
			string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

			using var doc = JsonDocument.Parse(jsonResponse);
			var geoObjects = doc.RootElement.GetProperty("response").GetProperty("GeoObjectCollection").GetProperty("featureMember");

			if(geoObjects.GetArrayLength() > 0)
			{
				var addressDetails = geoObjects[0].GetProperty("GeoObject");

				string fullAddress = addressDetails.GetProperty("metaDataProperty").GetProperty("GeocoderMetaData").GetProperty("text").GetString();

				return fullAddress;
			}
		}

		return "Address not found.";
	}

	#endregion
}