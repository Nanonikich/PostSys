using PostSys.Application.Contracts;
using PostSys.Application.Cqrs.Core;

namespace PostSys.Application.Commands.Postmen;

/// <summary>Представляет команду создания почтальона.</summary>
/// <param name="Fullname">Данные об имени почтальона.</param>
/// <param name="PackageId">Идентификатор посылки, которая принята почтальоном для доставки.</param>
/// <param name="Email">Email почтальона для доступа в систему.</param>
/// <param name="Password">Пароль почтальона для доступа в систему.</param>
public record CreatePostmanCommand(
	FullnameDto Fullname,
	Guid PackageId,
	string Email,
	string Password) : ICommand<Guid>;
