using System;

namespace Domain.EnvDte
{
	public class ProjectInfo
	{
		readonly string _fullName;

		readonly string _fileName;

		readonly string _uniqueName;

		public string FullName
		{
			get
			{
				return this._fullName;
			}
		}

		public string FileName
		{
			get
			{
				return this._fileName;
			}
		}

		public string UniqueName
		{
			get
			{
				return this._uniqueName;
			}
		}

		public ProjectInfo(string fullName, string fileName, string uniqueName)
		{
			this._fullName = fullName;
			this._fileName = fileName;
			this._uniqueName = uniqueName;
		}
	}
}