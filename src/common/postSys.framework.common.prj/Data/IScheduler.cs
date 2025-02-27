using System;

namespace PostSys.Framework.Common.Data;

/// <summary>Планировщик задач.</summary>
public interface IScheduler : IDisposable
{
	#region Methods

	/// <summary>Пытается запланировать выполнение действия на заданное время.</summary>
	/// <param name="executeAt">Время, когда необходимо выполнить действие.</param>
	/// <param name="action">Действие, которое будет выполнено в установленное время.</param>
	/// <returns>
	/// Ключ, по которому можно будет отменить запланированное действие или null, 
	/// если его запланировать не удалось.
	/// </returns>
	object? TrySchedule(DateTime executeAt, Action action);

	#endregion
}