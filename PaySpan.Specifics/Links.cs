using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpan.Specifics
{
	public static class Links
	{
		public static IDictionary<string, Uri> GetLinks()
		{
			return new Dictionary<string, Uri>
				{
					{ "Confluence", new Uri("http://confluence.payformance.net/") },
					{ "Jira", new Uri("http://jira/secure/Dashboard.jspa") },
					{ "Crucible", new Uri("http://jaxreview1:8060/") },
					{ "Stash", new Uri("http://jaxscm1.payformance.net:7990") }
				};
		}
	}
}
