namespace Domain.Adapters
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Linq;
	using System.Runtime.InteropServices;
	using System.Threading.Tasks;

	using Domain.Models;

	using Microsoft.Web.Administration;

	//[Export(typeof(IAdministerIIS))]
	public class IISAdministration : IAdministerIIS
	{
		[Export("IAdministerIISFactory", typeof(Func<string, IAdministerIIS>))]
		public static IAdministerIIS CreateAdmistrator(string server)
		{
			return new IISAdministration(server);
		}

		readonly string _server;

		//[ImportingConstructor]
		public IISAdministration(//[Import("Server")] 
			string server)
		{
			this._server = server;
		}

		public string StartSite(string site)
		{
			using (var iis = ServerManager.OpenRemote(this._server))
			{
				return iis.Sites.First(s => s.Name == site).Start().ToString();
			}
		}
	
		public string StopAppPool(string poolName)
		{
			using (var iis = ServerManager.OpenRemote(this._server))
			{
				return iis.ApplicationPools.First(ap => ap.Name == poolName).Stop().ToString();
			}
		}

		public string StartAppPool(string poolName)
		{
			using (var iis = ServerManager.OpenRemote(this._server))
			{
				return iis.ApplicationPools.First(ap => ap.Name == poolName).Start().ToString();
			}
		}

		public string AppPoolStatus(string poolName)
		{
			using (var iis = ServerManager.OpenRemote(this._server))
			{
				try
				{
					return iis.ApplicationPools.First(ap => ap.Name == poolName).State.ToString();
				}
				catch (System.UnauthorizedAccessException uaex)
				{

					return uaex.Message;
				}
				
			}
		}

		public string RecycleAppPool(string poolName)
		{
			using (var iis = ServerManager.OpenRemote(this._server))
			{
				try
				{
					return iis.ApplicationPools.First(ap => ap.Name == poolName).Recycle().ToString();
				}
				catch (COMException ex)
				{
					return ex.Message;
				}
				
			}
		}

		public Task<IEnumerable<IisAppInfo>> GetAppInfo()
		{
			return Task.Run(
				() =>
					{
						using (var iis = ServerManager.OpenRemote(this._server))
						{
							var q = from site in iis.Sites
							        let appName = site.Name
							        from app in site.Applications
							        let appPath = app.Path
							        let pool = app.ApplicationPoolName
							        let vd = app.VirtualDirectories.Where(v => v.Path != null)
							        let virtuals = vd.Select(v => new IisVirtualInfo(v.Path, v.PhysicalPath, v.PhysicalPath, exists: null))
							        select new IisAppInfo(appName + appPath, appPath, pool, virtuals);
							return q.ToArray().AsEnumerable();
							
						}
					});
		}
	}
}
