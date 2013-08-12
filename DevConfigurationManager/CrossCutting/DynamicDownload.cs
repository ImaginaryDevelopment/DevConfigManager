using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;

namespace DeveloperConfigurationManager.CrossCutting
{
    /// <summary>
	/// http://msdn.microsoft.com/en-us/library/ak58kz04.aspx
	/// </summary>
	[SecurityPermission(SecurityAction.Demand, ControlAppDomain = true)]
	class DynamicDownload
	{
		// Maintain a dictionary mapping DLL names to download file groups. This is trivial for this sample, 
		// but will be important in real-world applications where a feature is spread across multiple DLLs, 
		// and you want to download all DLLs for that feature in one shot. 
        readonly Dictionary<String, String> _dllMapping = new Dictionary<String, String>();

		public DynamicDownload()
		{
			_dllMapping["Domain.WebAdministration"] = "Domain.WebAdministration";
		    _dllMapping["Domain.EnvDte"] = "Domain.EnvDte";
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
		}

		System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			Assembly newAssembly = null;

			if(!ApplicationDeployment.IsNetworkDeployed)
			{
				return null;
				throw new FileLoadException("Cannot load assemblies dynamically - application is not deployed using ClickOnce");
			}
			var deploy = ApplicationDeployment.CurrentDeployment;

			var nameParts = args.Name.Split(',');
			var dllName = nameParts[0];
			string downloadGroupName = _dllMapping[dllName];
			try
			{
				deploy.DownloadFileGroup(downloadGroupName);
			}
			catch (DeploymentException de)
			{
				MessageBox.Show("Downloading file group failed. Group name: " + downloadGroupName + "; DLL name: " + args.Name+Environment.NewLine+de.Message);
				throw;
			}

				newAssembly = Assembly.LoadFile(Application.StartupPath + @"\" + dllName + ".dll");
			
			return newAssembly;
		}
	}
}
