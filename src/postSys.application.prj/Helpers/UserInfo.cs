namespace PostSys.Application.Helpers;

/// <summary>Информация о пользователе.</summary>
public class UserInfo
{
	/// <summary>Идентификатор пользователя.</summary>
	public int UserId { get; set; }

	/// <summary>Никнейм пользователя.</summary>
	public string UserUsername { get; set; }

	/// <summary>Роль пользователя в системе.</summary>
	public string UserRole { get; set; }
}
