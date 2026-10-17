using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using PostSys.Client.Data;

namespace PostSys.Client;

/// <summary>?????? ????????? ?????? ????? ??????.API ?? ???????????.</summary>
public sealed class AddressClient : IAddressClient
{
	private readonly HttpClient _httpClient;
	private Guid _apiKey;

	/// <summary>??????? ????????? ?????? <see cref="AddressClient"/>.</summary>
	/// <param name="httpClient">????? HTTP-?????? (?? ???????????? ???? ???????).</param>
	/// <param name="apiKey">???? ? ??????.API.</param>
	public AddressClient(HttpClient httpClient, Guid apiKey)
	{
		_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		_apiKey = apiKey;
	}

	/// <summary>????????? ???? ??????.API ??? ???????????? HTTP-???????.</summary>
	public void UpdateApiKey(Guid apiKey) => _apiKey = apiKey;

	/// <inheritdoc/>
	public async Task<string> GetAddressAsync(double latitude, double longitude, CancellationToken cancellationToken = default)
	{
		var url = $"https://geocode-maps.yandex.ru/1.x/?apikey={_apiKey}&geocode={longitude},{latitude}&format=json&lang=en-US";
		using var response = await _httpClient.GetAsync(url, cancellationToken);

		if(!response.IsSuccessStatusCode)
			return "Address not found.";

		await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
		using var doc = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);
		var geoObjects = doc.RootElement.GetProperty("response").GetProperty("GeoObjectCollection").GetProperty("featureMember");

		if(geoObjects.GetArrayLength() <= 0)
			return "Address not found.";

		return geoObjects[0]
			.GetProperty("GeoObject")
			.GetProperty("metaDataProperty")
			.GetProperty("GeocoderMetaData")
			.GetProperty("text")
			.GetString() ?? "Address not found.";
	}
}
