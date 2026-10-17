using PostSys.Application.Cqrs.Core;

namespace PostSys.Application.Commands.Postmen;

/// <summary>Определяет команду изменения Email почтальона.</summary>
/// <param name="Id">Идентификатор почтальона.</param>
/// <param name="Email">Email почтальона для обновления.</param>
public record ChangePostmanEmailCommand(Guid Id, string Email) : ICommand<bool>;