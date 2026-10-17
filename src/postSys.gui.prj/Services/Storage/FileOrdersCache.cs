using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using PostSys.Framework.Common;

namespace PostSys.Gui.Services.Storage;

/// <summary>Файловый кэш заказов.</summary>
public sealed class FileOrdersCache : IOrdersCache
{
    private readonly string _dir;

    /// <summary>Создаёт файловый кэш заказов в каталоге профиля приложения.</summary>
    public FileOrdersCache()
    {
        _dir = Path.Combine(ProfileLocationStorage.ProfileRootDir, "orders-cache");
        Directory.CreateDirectory(_dir);
    }

    /// <inheritdoc/>
    public async Task SaveAsync(Guid postmanId, IReadOnlyList<CachedOrder> orders, CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(_dir, $"{postmanId:N}.json");
        await using var stream = File.Create(path);
        await JsonSerializer.SerializeAsync(stream, orders, cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<CachedOrder>?> LoadAsync(Guid postmanId, CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(_dir, $"{postmanId:N}.json");
        if(!File.Exists(path))
            return null;

        await using var stream = File.OpenRead(path);
        return await JsonSerializer.DeserializeAsync<List<CachedOrder>>(stream, cancellationToken: cancellationToken);
    }
}