using PostSys.Gui.Common.ViewModels;
using PostSys.Gui.Common.Services.Dialogs;

namespace PostSys.Gui.ViewModels;

/// <summary>ћодель представлени€ текущего заказа.</summary>
public class CurrentOrderViewModel : ViewModelBase
{
	#region Properties

	/// <summary>¬озвращает взаимодействие с диалоговыми окнами.</summary>
	/// <value>¬заимодействие с диалоговыми окнами.</value>
	public DialogServiceInteractions DialogServiceInteractions { get; init; } = new();

	#endregion

	#region Methods

	/// <summary>ќчистка данных выбранного заказа.</summary>
	public void Clear()
	{

	}

	#endregion
}
