using System.Collections.Generic;

namespace Domain.Models
{
	public class IisAppInfo {
		readonly string _appName;
		readonly string _appPath;
		readonly string _pool;
		readonly IEnumerable<IisVirtualInfo> _virtuals;

		public string AppName
		{
			get { return _appName; }
		}

		public string AppPath
		{
			get { return _appPath; }
		}

		public string Pool
		{
			get { return _pool; }
		}

		public IEnumerable<IisVirtualInfo> Virtuals
		{
			get { return _virtuals; }
		}

		public IisAppInfo(string appName, string appPath, string pool, IEnumerable<IisVirtualInfo> virtuals)
		{
			_appName = appName;
			_appPath = appPath;
			_pool = pool;
			_virtuals = virtuals;
		}
	}
}