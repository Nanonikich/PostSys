using PostSys.Application.Contracts;
using PostSys.Application.Cqrs.Core;

namespace PostSys.Application.Commands.Postmen;

/// <summary>Определяет команду изменения данных об имени почтальона.</summary>
/// <param name="Id">Идентификатор почтальона.</param>
/// <param name="Fullname">Данные об имени почтальона для обновления.</param>
public record ChangePostmanFullnameCommand(Guid Id, FullnameDto Fullname) : ICommand<bool>;