using mUse.application.prj.Views.Forms;
using postSys.application.prj.Views.Forms;

namespace muse
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			ApplicationConfiguration.Initialize();
			new LoginForm().Show();
			Application.Run();
		}
	}
}