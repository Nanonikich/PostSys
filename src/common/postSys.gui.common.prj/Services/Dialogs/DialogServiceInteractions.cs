using ReactiveUI;

using PostSys.Gui.Common.Helpers;

namespace PostSys.Gui.Common.Services.Dialogs;

/// <summary>Создаёт взаимодействия открытия для моделей представления.</summary>
public class DialogServiceInteractions
{
	#region Properties

	/// <summary>Возвращает или задаёт взаимодействие с вопросом перед действием в приложении.</summary>
	/// <value>Взаимодействие с вопросом перед действием в приложении.</value>
	public Interaction<MessageContent, bool> ShowQuestionMessage { get; set; } = new();

	/// <summary>Возвращает или задаёт взаимодействие с уведомлением об ошибке в приложении.</summary>
	/// <value>Взаимодействие с уведомлением об ошибке в приложении.</value>
	public Interaction<MessageContent, bool> ShowErrorMessage { get; set; } = new();

	/// <summary>Возвращает или задаёт взаимодействие с закрываемым окном в приложении.</summary>
	/// <value>Взаимодействие с закрываемым окном в приложении.</value>
	public Interaction<bool, bool> CloseWindow { get; set; } = new();

	#endregion
}