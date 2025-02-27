using System;
using System.Collections.Generic;
using System.Threading;

using PostSys.Framework.Common.Data;

namespace PostSys.Framework.Common;

/// <summary>Планировщик задач.</summary>
public class Scheduler : IScheduler
{
	#region Helpers

	/// <summary>Запись, которая содержит сведения о запланированном действии.</summary>
	private sealed class Entry
	{
		/// <summary>Возвращает узел связанного списка, который содержит запись о запланированном действие.</summary>
		/// <value>Узел связанного списка, который содержит запись о запланированном действие.</value>
		public LinkedListNode<Entry> LinkedListNode { get; }

		/// <summary>Возвращает ключ, который идентифицирует запланированное действие.</summary>
		/// <value>Ключ, который идентифицирует запланированное действие.</value>
		public object Key { get; }

		/// <summary>Возвращает время, когда необходимо выполнить действие.</summary>
		/// <value>Время, когда необходимо выполнить действие.</value>
		public DateTime ActivationTime { get; }

		/// <summary>Действие, которое необходимо выполнить в момент времени <see cref="ActivationTime"/>.</summary>
		public Action Action { get; }

		/// <summary>Создаёт экземпляр класса <see cref="Entry"/>.</summary>
		/// <param name="key">Ключ, который идентифицирует запланированное действие.</param>
		/// <param name="activationTime">Время, когда необходимо выполнить действие.</param>
		/// <param name="action">Действие, которое необходимо выполнить в <paramref name="activationTime"/>.</param>
		public Entry(object? key, DateTime activationTime, Action action)
		{
			Key = key ?? this;
			ActivationTime = activationTime;
			Action = action;
			LinkedListNode = new LinkedListNode<Entry>(this);
		}
	}

	#endregion

	#region Data

	/// <summary>Словарь, который позволяет быстро найти запись о запланированном действие по её идентификатору.</summary>
	private readonly Dictionary<object, Entry> _lookup = [];

	/// <summary>Связанный список запланированных задач.</summary>
	private readonly LinkedList<Entry> _queue = [];

	/// <summary>Объект для синхронизации операций в классе.</summary>
	private readonly object _syncRoot = new();

	#endregion

	#region .ctor

	/// <summary>Создаёт экземпляр класса <see cref="Scheduler"/>.</summary>
	public Scheduler()
	{
		var thread = new Thread(ThreadProc)
		{
			Name = "Scheduler Thread",
			IsBackground = true
		};
		thread.Start();
	}

	#endregion

	#region Methods

	/// <inheritdoc/>
	public object? TrySchedule(DateTime executeAt, Action action)
	{
		var entry = new Entry(null, executeAt, action);
		if(!ScheduleCore(entry))
		{
			return null;
		}

		return entry.Key;
	}

	/// <summary>Точка входа в поток планировщика задач.</summary>
	private void ThreadProc()
	{
		Monitor.Enter(_syncRoot);
		try
		{
			while(!IsDisposed)
			{
				if(_queue.Count == 0)
				{
					Monitor.Wait(_syncRoot);
					continue;
				}

				var value = _queue.First.Value;
				TimeSpan timeSpan = value.ActivationTime - DateTime.Now;
				if(timeSpan.Ticks <= 0)
				{
					_queue.RemoveFirst();
					_lookup.Remove(value.Key);
					value.Action();
				}
				else
				{
					int millisecondsTimeout = Math.Max(10, Math.Min(60000, (int)timeSpan.TotalMilliseconds));
					Monitor.Wait(_syncRoot, millisecondsTimeout);
				}
			}
		}
		finally
		{
			Monitor.Exit(_syncRoot);
		}
	}

	/// <summary>Пытается запланировать задачу на выполнение.</summary>
	/// <param name="entry">Запись на выполнение, которую необходимо запланировать.</param>
	/// <returns>Успешность планирования задачи на выполнение.</returns>
	private bool ScheduleCore(Entry entry)
	{
		Monitor.Enter(_syncRoot);
		try
		{
			if(IsDisposed)
			{
				return false;
			}

			if(_lookup.TryGetValue(entry.Key, out Entry value))
			{
				if(value.LinkedListNode == _queue.First)
				{
					Monitor.Pulse(_syncRoot);
					_queue.RemoveFirst();
				}
				else
				{
					_queue.Remove(value.LinkedListNode);
				}

				InsertEntry(entry);
				_lookup[entry.Key] = entry;
			}
			else
			{
				InsertEntry(entry);
				_lookup.Add(entry.Key, entry);
				if(_queue.Count == 1)
				{
					Monitor.Pulse(_syncRoot);
				}
			}
		}
		finally
		{
			Monitor.Exit(_syncRoot);
		}

		return true;
	}

	/// <summary>Вставляет новую запись о запланированном действие.</summary>
	/// <param name="entry">Запись о запланированном действие.</param>
	private void InsertEntry(Entry entry)
	{
		if(_queue.Count == 0 || _queue.Last.Value.ActivationTime <= entry.ActivationTime)
		{
			_queue.AddLast(entry.LinkedListNode);
			return;
		}

		for(LinkedListNode<Entry> linkedListNode = _queue.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if(linkedListNode.Value.ActivationTime > entry.ActivationTime)
			{
				_queue.AddBefore(linkedListNode, entry.LinkedListNode);
				return;
			}
		}

		_queue.AddLast(entry.LinkedListNode);
	}

	#endregion

	#region IDisposable

	/// <summary>Возвращает или задаёт флаг уничтожены ли зависимости.</summary>
	/// <value>Флаг уничтожены ли зависимости.</value>
	public bool IsDisposed { get; private set; }

	/// <inheritdoc/>
	public void Dispose()
	{
		if(IsDisposed)
		{
			return;
		}

		Monitor.Enter(_syncRoot);
		try
		{
			if(!IsDisposed)
			{
				_queue.Clear();
				_lookup.Clear();
				IsDisposed = true;
				Monitor.Pulse(_syncRoot);
			}
		}
		finally
		{
			Monitor.Exit(_syncRoot);
		}
	}

	#endregion
}