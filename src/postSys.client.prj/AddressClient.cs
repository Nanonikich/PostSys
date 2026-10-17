using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using PostSys.Client.Data;

namespace PostSys.Client;

/// <summary> лиент получени€ адреса через яндекс.API по координатам.</summary>
public class AddressClient : IAddressClient
{
	#region Data

	private readonly Guid _apiKey;

	#endregion

	#region .ctor

	/// <summary>—оздаЄт экземпл€р класса <see cref="AddressClient"/>.</summary>
	/// <param name="apiKey"> люч к яндекс.API.</param>
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

				return addressDetails
					.GetProperty("metaDataProperty")
					.GetProperty("GeocoderMetaData")
					.GetProperty("text")
					.GetString() ?? "Address not found.";
			}
		}

		return "Address not found.";
	}

	#endregion
}