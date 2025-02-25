using System;

using PostSys.Service.Common.Cqrs.Core;

namespace PostSys.Service.Commands.Packages;

/// <summary>Представляет команду изменения данных о посылке.</summary>
/// <param name="Id">Идентификатор посылки.</param>
/// <param name="ClientId">Идентификатор клиента для обновления.</param>
/// <param name="PostmanId">Идентификатор почтальона для обновления.</param>
public record ChangePackageCommand(Guid Id, Guid ClientId, Guid PostmanId) : ICommand<bool>;