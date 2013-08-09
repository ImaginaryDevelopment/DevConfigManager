using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapters
{
	using System.Threading;

	using Domain.Extensions;

	public static class RCmd
	{
		public static void Prompt(string server)
		{
			Process.Prompt("psexec " + server.EnsureStartsWith(@"\\") + " cmd.exe /k");
		}

		public static PsStreamingOuts RunProcessRedirectedObserverable(string server, string filename, string args, CancellationToken ct, string input)
		{
			return Process.RunRedirectedObservable("psexec", server.EnsureStartsWith(@"\\") + " " + filename + " " + args, ct, input,null);
		}
	}
}
