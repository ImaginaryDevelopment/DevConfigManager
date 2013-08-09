namespace Domain.Models
{
	public class IisVirtualInfo {
		readonly string _virDir;
		readonly string _physicalPath;
		readonly string _displayPhysicalPath;
		readonly bool? _exists;

		public string VirDir
		{
			get { return _virDir; }
		}

		public string PhysicalPath
		{
			get { return _physicalPath; }
		}

		public string DisplayPhysicalPath
		{
			get { return _displayPhysicalPath; }
		}

		public bool? Exists
		{
			get { return _exists; }
		}

		public IisVirtualInfo(string virDir, string physicalPath, string displayPhysicalPath, bool? exists)
		{
			_virDir = virDir;
			_physicalPath = physicalPath;
			_displayPhysicalPath = displayPhysicalPath;
			_exists = exists;
		}
	}
}