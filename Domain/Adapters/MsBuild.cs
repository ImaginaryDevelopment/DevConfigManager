using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Extensions;
using Domain.Models;
namespace Domain.Adapters
{
	public class MsBuild:IRunnable
	{
		string _exePath=Path.Combine(Environment.ExpandEnvironmentVariables("%windir%"),@"Microsoft.NET\Framework\v4.0.30319","msbuild.exe");

		public MsBuild(string args,TimeSpan timeout, string workingDirectory=null)
		{
			Args = args;
			Timeout = timeout;
			WorkingDirectory = workingDirectory;
			
		}
		
		public string ExePath { protected get { return _exePath; } 
			set
			{
				if (_exePath.IsNullOrEmpty())
				{
					throw new ArgumentException("exePath");
				}
				if (System.IO.File.Exists(_exePath) == false)
				{
					throw new FileNotFoundException("exePath");
				}

				_exePath = value;
			}
		}

		public string Args { get; set; }
		public string WorkingDirectory { get; set; }
		public string Filename { get { return ExePath; } }

		public TimeSpan Timeout { get; private set; }
	}
}
