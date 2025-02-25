using System;

using PostSys.ReadModels.Helpers;
using PostSys.Service.Common.Cqrs.Core;

namespace PostSys.Service.Commands.Postmen;

/// <summary>Определяет команду изменения данных об имени почтальона.</summary>
/// <param name="Id">Идентификатор почтальона.</param>
/// <param name="Fullname">Данные об имени почтальона для обновления.</param>
public record ChangePostmanFullnameCommand(Guid Id, Fullname Fullname) : ICommand<bool>;