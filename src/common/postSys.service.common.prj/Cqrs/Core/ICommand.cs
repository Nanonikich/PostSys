using MediatR;

namespace PostSys.Service.Common.Cqrs.Core;

/// <summary>Определяет команду.</summary>
public interface ICommand : IRequest;

/// <summary>Определяет команду с результатом выполнения.</summary>
/// <typeparam name="TResult">Результат.</typeparam>
/// <remarks>Возвращаем идентификаторы, метаданные, версии агрегатов, но не бизнес данные.</remarks>
public interface ICommand<out TResult> : IRequest<TResult>;