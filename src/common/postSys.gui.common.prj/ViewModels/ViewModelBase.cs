using System.ComponentModel;
using System.Runtime.CompilerServices;

using ReactiveUI;

namespace PostSys.Gui.Common.ViewModels;

/// <summary>
/// Базовый класс для ViewModel, реализующий интерфейс INotifyPropertyChanged.
/// </summary>
public class ViewModelBase : ReactiveObject
{
	/// <summary>Происходит, когда свойство изменяется.</summary>
	public event PropertyChangedEventHandler PropertyChanged;

	/// <summary>Вызывает событие <see cref="PropertyChanged"/>, чтобы сообщить об изменении свойства.</summary>
	/// <param name="propertyName">
	/// Имя изменённого свойства. Если не указано, имя будет получено автоматически с помощью <see cref="CallerMemberNameAttribute"/>.
	/// </param>
	protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}