using MediatR;

namespace PostSys.Service.Common.Cqrs.Core;

/// <summary>Определяет обработчик команды.</summary>
/// <typeparam name="TCommand">Команда.</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> 
	where TCommand : ICommand;

/// <summary>Определяет обработчик команды.</summary>
/// <typeparam name="TCommand">Команда.</typeparam>
/// <typeparam name="TResult">Результат выполнения команды.</typeparam>
public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
	where TCommand : ICommand<TResult>;