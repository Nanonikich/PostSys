using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using PostSys.Framework.Common;

namespace PostSys.Gui.Services.Storage;

/// <summary>Защищённое хранилище сеанса с шифрованием в каталоге приложения.</summary>
public sealed class AesSecureSessionStore : ISecureSessionStore
{
    private readonly string _filePath;
    private readonly string _keyPath;

    /// <summary>Создаёт экземпляр класса <see cref="AesSecureSessionStore"/>.</summary>
    public AesSecureSessionStore()
    {
        var dir = Path.Combine(ProfileLocationStorage.ProfileRootDir, "secure");
        Directory.CreateDirectory(dir);
        _filePath = Path.Combine(dir, "session.bin");
        _keyPath = Path.Combine(dir, "session.key");
    }

    /// <inheritdoc/>
    public async Task SaveAsync(SecureSession session, string? passwordToRemember, CancellationToken cancellationToken = default)
    {
        var payload = new StoredPayload(
            session.Email,
            session.AccessToken,
            session.PostmanId,
            session.ExpiresAt,
            passwordToRemember);

        var json = JsonSerializer.SerializeToUtf8Bytes(payload);
        var key = await GetOrCreateKeyAsync(cancellationToken);
        var nonce = RandomNumberGenerator.GetBytes(12);
        var cipher = new byte[json.Length];
        var tag = new byte[16];
        using(var aes = new AesGcm(key, 16))
            aes.Encrypt(nonce, json, cipher, tag);

        var blob = new byte[nonce.Length + tag.Length + cipher.Length];
        Buffer.BlockCopy(nonce, 0, blob, 0, nonce.Length);
        Buffer.BlockCopy(tag, 0, blob, nonce.Length, tag.Length);
        Buffer.BlockCopy(cipher, 0, blob, nonce.Length + tag.Length, cipher.Length);
        await File.WriteAllBytesAsync(_filePath, blob, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<SecureSession?> LoadAsync(CancellationToken cancellationToken = default)
    {
        var payload = await LoadPayloadAsync(cancellationToken);
        if(payload is null)
            return null;

        return new(
            payload.Email,
            payload.AccessToken,
            payload.PostmanId,
            payload.ExpiresAt,
            null);
    }

    /// <inheritdoc/>
    public async Task<(string Email, string Password)?> LoadCredentialsAsync(CancellationToken cancellationToken = default)
    {
        var payload = await LoadPayloadAsync(cancellationToken);
        if(payload is null || string.IsNullOrEmpty(payload.Password))
            return null;

        return (payload.Email, payload.Password);
    }

    /// <inheritdoc/>
    public Task ClearAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if(File.Exists(_filePath))
                File.Delete(_filePath);
        }
        catch { }

        return Task.CompletedTask;
    }

    private async Task<StoredPayload?> LoadPayloadAsync(CancellationToken cancellationToken)
    {
        if(!File.Exists(_filePath) || !File.Exists(_keyPath))
            return null;

        try
        {
            var blob = await File.ReadAllBytesAsync(_filePath, cancellationToken);
            if(blob.Length < 28)
                return null;

            var key = await File.ReadAllBytesAsync(_keyPath, cancellationToken);
            var nonce = blob.AsSpan(0, 12).ToArray();
            var tag = blob.AsSpan(12, 16).ToArray();
            var cipher = blob.AsSpan(28).ToArray();
            var plain = new byte[cipher.Length];
            using(var aes = new AesGcm(key, 16))
                aes.Decrypt(nonce, cipher, tag, plain);

            return JsonSerializer.Deserialize<StoredPayload>(plain);
        }
        catch
        {
            return null;
        }
    }

    private async Task<byte[]> GetOrCreateKeyAsync(CancellationToken cancellationToken)
    {
        if(File.Exists(_keyPath))
            return await File.ReadAllBytesAsync(_keyPath, cancellationToken);

        var key = RandomNumberGenerator.GetBytes(32);
        await File.WriteAllBytesAsync(_keyPath, key, cancellationToken);
        return key;
    }

    private sealed record StoredPayload(
        string Email,
        string AccessToken,
        Guid PostmanId,
        DateTimeOffset ExpiresAt,
        string? Password);
}