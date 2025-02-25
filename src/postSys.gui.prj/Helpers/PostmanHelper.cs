using System;

namespace PostSys.Gui.Helpers;

/// <summary>Данные об авторизованном почтальоне.</summary>
public class PostmanHelper
{
	#region Properties

	/// <summary>Возвращает или задаёт идентификатор почтальона.</summary>
	/// <value>Идентификатор почтальона.</value>
	public Guid PostmanId { get; set; }

	#endregion
}