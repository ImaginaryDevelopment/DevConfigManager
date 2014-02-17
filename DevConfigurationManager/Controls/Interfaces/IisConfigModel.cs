using System;

namespace DeveloperConfigurationManager.Controls.Interfaces
{
	public class IisConfigModel {
		readonly string _appName;
		readonly string _appPath;
		readonly string _pool;
	    readonly int? _pid;
	    string _physicalPath;
		readonly Action<string> _pathSetter;
		readonly string _virDir;

		private readonly bool _exists;

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

		public string PhysicalPath
		{
			get { return _physicalPath; }
			set { _pathSetter(value);
				_physicalPath = value;
			}
		}

		public string VirDir
		{
			get { return _virDir; }
		}

		public bool Exists
		{
			get
			{
				return this._exists;
			}
		}

	    public int? Pid
	    {
	        get { return _pid; }
	    }

	    public IisConfigModel(string appName, string appPath, string pool,int? pid, string physicalPath,Action<string> pathSetter, string virDir,bool exists)
		{
			
			_appName = appName;
			_appPath = appPath;
			_pool = pool;
		    _pid = pid;
		    _physicalPath = physicalPath;
			_pathSetter = pathSetter;
			_virDir = virDir;
			this._exists = exists;
		}
	}
}