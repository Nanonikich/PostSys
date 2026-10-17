using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace PostSys.Client.GraphQl;

/// <summary>Общее подключение к службе запросов (один <see cref="HttpClient"/> на приложение).</summary>
public sealed class GraphQlConnection : IAsyncDisposable
{
    private readonly HttpClient _httpClient;
    private readonly SystemTextJsonSerializer _serializer = new();
    private readonly object _sync = new();
    private GraphQLHttpClient? _client;
    private string _baseAddress;
    private string? _accessToken;
    private bool _disposed;

    /// <summary>Создаёт подключение к службе.</summary>
    /// <param name="baseAddress">Базовый адрес службы.</param>
    /// <param name="httpClient">HTTP-клиент приложения.</param>
    public GraphQlConnection(string baseAddress, HttpClient httpClient)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(baseAddress);
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _baseAddress = baseAddress.TrimEnd('/');
    }

    /// <summary>Базовый адрес службы.</summary>
    public string BaseAddress
    {
        get
        {
            lock(_sync)
                return _baseAddress;
        }
    }

    /// <summary>Текущий ключ доступа (если задан).</summary>
    public string? AccessToken
    {
        get
        {
            lock(_sync)
                return _accessToken;
        }
    }

    /// <summary>Возвращает клиент запросов (создаётся при первом обращении).</summary>
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

    /// <summary>Устанавливает или сбрасывает ключ доступа в заголовке авторизации.</summary>
    /// <param name="accessToken">Ключ доступа или <see langword="null"/> для сброса.</param>
    public void SetAccessToken(string? accessToken)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        lock(_sync)
        {
            _accessToken = string.IsNullOrWhiteSpace(accessToken) ? null : accessToken;
            ApplyAuthHeader();
        }
    }

    /// <summary>Пересоздаёт клиент при смене адреса службы.</summary>
    /// <param name="baseAddress">Новый базовый адрес службы.</param>
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
            ApplyAuthHeader();
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
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        _disposed = true;
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    private void ApplyAuthHeader()
    {
        _httpClient.DefaultRequestHeaders.Authorization = _accessToken is null
            ? null
            : new("Bearer", _accessToken);
    }

    private GraphQLHttpClient CreateClient(string baseAddress)
    {
        ApplyAuthHeader();
        var options = new GraphQLHttpClientOptions
        {
            EndPoint = new Uri(baseAddress + "/graphql"),
            WebSocketEndPoint = new Uri(baseAddress.Replace("https://", "wss://", StringComparison.OrdinalIgnoreCase)
                .Replace("http://", "ws://", StringComparison.OrdinalIgnoreCase) + "/graphql")
        };

        return new(options, _serializer, _httpClient);
    }
}