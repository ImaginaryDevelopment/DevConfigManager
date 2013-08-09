using System;
using System.Threading.Tasks;

namespace DeveloperConfigurationManager.Controls.Interfaces
{
	public interface IRefreshable : INamed
	{
		Task RefreshData();
		string Status { get;  }
		DateTime? Refreshed { get; }
	}
}
