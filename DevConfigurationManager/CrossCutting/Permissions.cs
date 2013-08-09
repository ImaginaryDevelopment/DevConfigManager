using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;

namespace DeveloperConfigurationManager.CrossCutting
{
    /// <summary>
	/// http://antscode.blogspot.com/2011/02/running-clickonce-application-as.html
	/// </summary>
	internal static class Permissions
	{
		internal static bool IsRunAsAdministrator()
		{
			using (var wi = WindowsIdentity.GetCurrent())
			{
				var wp = new WindowsPrincipal(wi);

				return wp.IsInRole(WindowsBuiltInRole.Administrator);
			}
		}

		internal static bool TryAsAdministrator(bool required = false)
		{
			if (IsRunAsAdministrator()) return true;
			// It is not possible to launch a ClickOnce app as administrator directly, so instead we launch the
			// app as administrator in a new process.
			var processInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase);

			// The following properties run the new process as administrator
			processInfo.UseShellExecute = true;
			processInfo.Verb = "runas";

			// Start the new process
			try
			{
				Process.Start(processInfo);
				// Shut down the current process
				Application.Exit();
				return true;
			}
			catch (Exception)
			{
				if (required)
				{
					// The user did not allow the application to run as administrator
					MessageBox.Show("Sorry, this application must be run as Administrator.");
					throw;
				}

			}
			return false;

		}

	}
}
