using System;

using PostSys.Service.Common.Cqrs.Core;

namespace PostSys.Service.Commands.Postmen;

/// <summary>Определяет команду изменения данных почтальона.</summary>
/// <param name="Id">Идентификатор почтальона.</param>
/// <param name="PostmanPackageId">Идентификатор посылки.</param>
public record ChangePostmanCommand(Guid Id, Guid PostmanPackageId) : ICommand<bool>;