using PostSys.Gui.Common.ViewModels;
using PostSys.Gui.Common.Services.Dialogs;

namespace PostSys.Gui.ViewModels;

/// <summary>Модель представления текущего заказа.</summary>
public class CurrentOrderViewModel : ViewModelBase
{
	#region Properties

	/// <summary>Возвращает взаимодействие с диалоговыми окнами.</summary>
	/// <value>Взаимодействие с диалоговыми окнами.</value>
	public DialogServiceInteractions DialogServiceInteractions { get; init; } = new();

	#endregion

	#region Methods

	/// <summary>Очистка данных выбранного заказа.</summary>
	public void Clear()
	{

	}

	#endregion
}
