namespace Domain.Models
{
	public class IisVirtualInfo {
		readonly string _virDir;
		readonly string _physicalPath;
		readonly string _displayPhysicalPath;
	    private readonly int? _pid;
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

	    public int? Pid
	    {
	        get { return _pid; }
	    }

	    public IisVirtualInfo(string virDir, string physicalPath, string displayPhysicalPath,int? pid, bool? exists)
		{
			_virDir = virDir;
			_physicalPath = physicalPath;
			_displayPhysicalPath = displayPhysicalPath;
		    _pid = pid;
		    _exists = exists;
		}
	}
}