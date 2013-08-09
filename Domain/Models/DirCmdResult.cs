using System;
using System.Collections.Generic;

namespace Domain.Models
{
	using System.Threading.Tasks;

	public class DirCmdResult
	{
		public IEnumerable<DirInfo> Directories { get; set; }
		public string Working { get; set; }

	}
	public class FullInfo
	{
		public GitInfo GitInfo { get; set; }
		public DirInfo DirInfo { get; set; }

	}
	public class FullInfoAsync
	{
		public GitInfoAsync GitInfo { get; set; }
		public DirInfo DirInfo { get; set; }

	}
	public class GitInfoAsync
	{
		public bool IsGit { get; set; }
		public Task<string> Remotes { get; set; }

	}
	public class GitInfo
	{
		public bool IsGit { get; set; }
		public string Remotes { get; set; }
		override public string ToString()
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(this);
		}
	}
	public sealed class DirInfo
	{
		public DateTime Date { get; set; }

		public string Type { get; set; }

		public string Name { get; set; }

		public string Target { get; set; }

		public override bool Equals(object obj)
		{
			
			var target = obj as DirInfo;
			if (target == null) return false;
			return target.Date == this.Date && target.Type == this.Type && target.Name == this.Name
			       && target.Target == this.Target;
		}
	}
}
