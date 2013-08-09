using System;
using System.Linq;
using System.Text.RegularExpressions;
using Domain.Extensions;
using Domain.Models;

namespace Domain.Adapters
{
	using System.Threading;
	using System.Threading.Tasks;

	public class DirectoryCmd
	{
		readonly string _directory;
		static readonly Regex DirRegex = new Regex(@"([0-9]{2}/[0-9]{2}/[0-9]{4}\s+[0-9]{2}:[0-9]{2} [AP]M)\s+ <([A-Z]+)>\s+(.*)" + Environment.NewLine);
		
		public DirectoryCmd(string directory)
		{
			_directory = directory;
			
		}

		public string Directory
		{
			get { return _directory; }
		}

		public async Task<DirCmdResult> GetDirInfo(CancellationToken ct)
		{

			var cmdResult = Process.Cmd("dir", ct, Directory);
			await cmdResult.AwaitAsync();

			return new DirCmdResult()
			{

				Directories = from m in DirRegex.Matches(cmdResult.GetOutput()).Cast<Match>()
								  let dtTime = m.Groups[1].Value
								  let nameColumn = m.Groups[3].Value
								  let name = nameColumn.BeforeOrSelf(" [")
								  let junctionTarget = (nameColumn.Contains(" [") ? nameColumn.After(" [").Before("]") : null)
								  select new DirInfo { Name = name, Date = DateTime.Parse(dtTime), Type = m.Groups[2].Value, Target = junctionTarget }
			};



		}
	}
}
