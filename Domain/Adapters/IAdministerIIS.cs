namespace Domain.Adapters
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Domain.Models;

	public interface IAdministerIIS
	{
		string StartSite(string site);

		string StopAppPool(string poolName);

		string StartAppPool(string poolName);

		string AppPoolStatus(string poolName);

		string RecycleAppPool(string poolName);

		Task<IEnumerable<IisAppInfo>> GetAppInfo();
	}
}