using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace PostSys.Client.GraphQl;

/// <summary>Общее HTTP/GraphQL-подключение к сервису (один <see cref="HttpClient"/> на приложение).</summary>
public sealed class GraphQlConnection : IAsyncDisposable
{
	private readonly HttpClient _httpClient;
	private readonly SystemTextJsonSerializer _serializer = new();
	private readonly object _sync = new();
	private GraphQLHttpClient? _client;
	private string _baseAddress;
	private bool _disposed;

	/// <summary>Создаёт подключение.</summary>
	/// <param name="baseAddress">Базовый адрес сервиса (без /graphql).</param>
	/// <param name="httpClient">Общий HTTP-клиент.</param>
	public GraphQlConnection(string baseAddress, HttpClient httpClient)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(baseAddress);
		_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		_baseAddress = baseAddress.TrimEnd('/');
	}

	/// <summary>Базовый адрес сервиса.</summary>
	public string BaseAddress
	{
		get
		{
			lock(_sync)
				return _baseAddress;
		}
	}

	/// <summary>Возвращает GraphQL-клиент (создаётся лениво).</summary>
	public GraphQLHttpClient Client
	{
		get
		{
			ObjectDisposedException.ThrowIf(_disposed, this);
			lock(_sync)
			{
				_client ??= CreateClient(_baseAddress);
				return _client;
			}
		}
	}

	/// <summary>Пересоздаёт клиент при смене адреса сервиса.</summary>
	public void UpdateBaseAddress(string baseAddress)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(baseAddress);
		ObjectDisposedException.ThrowIf(_disposed, this);

		lock(_sync)
		{
			var normalized = baseAddress.TrimEnd('/');
			if(string.Equals(_baseAddress, normalized, StringComparison.OrdinalIgnoreCase) && _client is not null)
				return;

			_baseAddress = normalized;
			if(Uri.TryCreate(normalized, UriKind.Absolute, out var uri))
				_httpClient.BaseAddress = uri;

			_client?.Dispose();
			_client = null;
		}
	}

	/// <inheritdoc/>
	public ValueTask DisposeAsync()
	{
		if(_disposed)
			return ValueTask.CompletedTask;

		lock(_sync)
		{
			_client?.Dispose();
			_client = null;
		}

		_disposed = true;
		GC.SuppressFinalize(this);
		return ValueTask.CompletedTask;
	}

	private GraphQLHttpClient CreateClient(string baseAddress)
	{
		var options = new GraphQLHttpClientOptions
		{
			EndPoint = new Uri(baseAddress + "/graphql"),
			WebSocketEndPoint = new Uri(baseAddress.Replace("https://", "wss://", StringComparison.OrdinalIgnoreCase)
				.Replace("http://", "ws://", StringComparison.OrdinalIgnoreCase) + "/graphql")
		};

		// HttpClient принадлежит этому соединению / DI и не должен уничтожаться клиентом GraphQL.
		return new GraphQLHttpClient(options, _serializer, _httpClient);
	}
}
