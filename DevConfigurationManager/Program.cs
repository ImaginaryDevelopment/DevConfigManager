using System;
using System.Windows.Forms;
using DeveloperConfigurationManager.CrossCutting;

namespace DeveloperConfigurationManager
{
    static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			var isAdmin = Permissions.IsRunAsAdministrator();
			if (!isAdmin && Permissions.TryAsAdministrator())
			{
				Application.Exit();
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			NinjectConfig.Start();
			NinjectConfig.Run(isAdmin, Application.Run);
			
		}
	}
}
