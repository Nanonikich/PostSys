using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PostSys.Gui.Services.Storage;

/// <summary>Сохранённое описание заказа для автономного режима.</summary>
/// <param name="PackageId">Идентификатор посылки.</param>
/// <param name="ClientId">Идентификатор клиента.</param>
/// <param name="Status">Статус посылки.</param>
/// <param name="Address">Текстовый адрес.</param>
/// <param name="ClientFullname">ФИО клиента.</param>
/// <param name="PhoneNumber">Телефон клиента.</param>
/// <param name="ImagePath">Путь к изображению посылки.</param>
public sealed record CachedOrder(
    Guid PackageId,
    Guid ClientId,
    string Status,
    string Address,
    string ClientFullname,
    string PhoneNumber,
    string ImagePath);

/// <summary>Локальный кэш списка заказов.</summary>
public interface IOrdersCache
{
    /// <summary>Сохраняет список заказов почтальона.</summary>
    /// <param name="postmanId">Идентификатор почтальона.</param>
    /// <param name="orders">Список заказов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача сохранения.</returns>
    Task SaveAsync(Guid postmanId, IReadOnlyList<CachedOrder> orders, CancellationToken cancellationToken = default);

    /// <summary>Загружает сохранённые заказы почтальона.</summary>
    /// <param name="postmanId">Идентификатор почтальона.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список заказов или <see langword="null"/>.</returns>
    Task<IReadOnlyList<CachedOrder>?> LoadAsync(Guid postmanId, CancellationToken cancellationToken = default);
}