using PostSys.Application.Views.Forms;

namespace PostSys.Application;

public static class Program
{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main()
	{
		ApplicationConfiguration.Initialize();
		new LoginForm().Show();
		System.Windows.Forms.Application.Run();
	}
}