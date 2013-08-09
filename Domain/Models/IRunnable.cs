using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public interface IRunnable
	{
		string Args { get; }
		string WorkingDirectory { get; }
		string Filename { get;  }

		TimeSpan Timeout { get; }
	}
}
