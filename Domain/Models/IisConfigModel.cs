namespace Domain.Models
{
	using System;

	public class IisConfigModel {
		readonly string _appName;
		readonly string _appPath;
		readonly string _pool;
		string _physicalPath;
		readonly Action<string> _pathSetter;
		readonly string _virDir;

		private readonly bool _exists;

		public string AppName
		{
			get { return this._appName; }
		}

		public string AppPath
		{
			get { return this._appPath; }
		}

		public string Pool
		{
			get { return this._pool; }
		}

		public string PhysicalPath
		{
			get { return this._physicalPath; }
			set { 
				this._pathSetter(value);
				this._physicalPath = value;
			}
		}

		public string VirDir
		{
			get { return this._virDir; }
		}

		public bool Exists
		{
			get
			{
				return this._exists;
			}
		}

		public IisConfigModel(string appName, string appPath, string pool, string physicalPath, Action<string> pathSetter, string virDir, bool exists)
		{
			
			this._appName = appName;
			this._appPath = appPath;
			this._pool = pool;
			this._physicalPath = physicalPath;
			this._pathSetter = pathSetter;
			this._virDir = virDir;
			this._exists = exists;
		}
	}
}