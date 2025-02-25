using System;

using PostSys.Service.Common.Cqrs.Core;

namespace PostSys.Service.Commands.Postmen;

/// <summary>Определяет команду изменения пароля почтальона.</summary>
/// <param name="Id">Идентификатор почтальона.</param>
/// <param name="Password">Пароль почтальона для обновления.</param>
public record ChangePostmanPasswordCommand(Guid Id, string Password) : ICommand<bool>;