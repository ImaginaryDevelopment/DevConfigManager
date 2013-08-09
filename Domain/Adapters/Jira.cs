using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Models;

namespace Domain.Adapters
{
	public class Jira
	{
		readonly ILog _logger;

		readonly ICanDownload _downloader;

		public Jira(ILog logger, ICanDownload downloader)
		{
			_logger = logger;
			_downloader = downloader;
		}
		public async Task<object> GetIssue(string authority,string issueId)
		{
			var url = "http://" + authority + "/rest/api/latest/issue/";
			var fullUrl = url + issueId;
			var rawJson = await _downloader.Download(new Uri(fullUrl), "application/json");
			return rawJson;
		}
	}
}
