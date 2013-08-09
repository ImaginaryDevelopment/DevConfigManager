using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapters
{
	using System.Threading;

	using Domain.Extensions;

	public class Sc
	{
		public struct ScQueryOutput
		{
			public string ServiceName { get; set; }
			public string DisplayName { get; set; }
			public string Type { get; set; }
			public string State { get; set; }
			public string Unmapped { get; set; }
		}

		public static StreamingOuts Query(string server, CancellationToken ct)
		{
			return Domain.Adapters.Process.RunRedirectedObservable("sc", @"\\" + server + " query state= all type= service", ct);
		}

		public static StreamingOuts Run(string server, string command, string service, CancellationToken ct)
		{
			return Domain.Adapters.Process.RunRedirectedObservable("sc", @"\\" + server + " " + command + " " + service, ct);
		}

		public static IEnumerable<ScQueryOutput> TransformScQuery(string output)
		{
			var grouped = output.SplitLines().SkipWhile(s=>string.IsNullOrEmpty(s) || s.StartsWith("SERVICE")==false).GroupLinesBy("SERVICE_NAME");
			foreach (var line in grouped
						.Select(g => g.SplitLines()
							.Select(l => l.AfterOrSelf(": "))
						.ToArray()))
			{

				var serviceName = line[0];
				yield return new ScQueryOutput()
				{
					ServiceName = serviceName,
					DisplayName = line[1],
					State = line[3] + line[4],
					Type = line[2],
					Unmapped = line.Skip(5).Delimit(Environment.NewLine)
				};

			}

		}
	}
}
