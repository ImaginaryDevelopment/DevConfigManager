using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapters
{
	using System.Collections;

	using Domain.Extensions;
	using Domain.Helpers;
	using Domain.Models;
	using Domain.Models.Stash;

	public class Stash
	{
		readonly ILog _logger;

		readonly ICanDownload _downloader;

		public Stash(ILog logger, ICanDownload downloader)
		{
			_logger = logger;
			_downloader = downloader;
		}

		public async Task<IEnumerable<StashPullRequestInfo>> GetOpenPullRequests(string currentUser, string authority, params KeyValuePair<string, StashRestUrl>[] nameRepoPairs)
		{
			var l = new List<StashPullRequestInfo>();
			foreach (var item in nameRepoPairs)
			{
				var pullUrl = new StashUrl(item.Value);
				var info = await _downloader.Download(item.Value.GetOpenReviewsUri(), "application/json");
				_logger.Log(JsonPrettifier.PrettyPrint(info)+Environment.NewLine);
				if (string.IsNullOrWhiteSpace(info))
					continue;
				var response = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, dynamic>>(info);
				foreach (dynamic v in response["values"])
				{
					var raw = ((Newtonsoft.Json.Linq.JArray)v.reviewers).ToArray().Cast<dynamic>();
					var reviewers = (from d in raw.ToArray()
										  select
											  new { Username = (string)d.user.name, Role = (string)d.role, Approved = (bool)d.approved }).ToArray();
					var id = (string)v.id;

					var completeCount = -1;
					var notApproved = new List<string>();
					foreach (var r in reviewers)
					{
						if (completeCount < 0) completeCount++;
						if (r.Approved) { completeCount++; }
						else
						{

							//new{ name,role}.Dump("name/role");
							if (r.Role != "AUTHOR")
								notApproved.Add(r.Username);
						}
					}
					var participating = reviewers.Any(r => r.Username.StrComp(currentUser, true) == 0);
					var approvedByMe = participating && notApproved.Contains(currentUser,StringComparer.CurrentCultureIgnoreCase)==false;
					var needsAction= participating && !approvedByMe || (completeCount > 2
					                       && StringComparer.CurrentCultureIgnoreCase.Compare((string)v.author.user.name, currentUser)
					                       == 0);
					var x = new StashPullRequestInfo(
						id,
						(string)v.author.user.displayName,
						completeCount,
						(string)v.title,
						(string)v.description,
						(string)v.state,
						reviewers.Select(s => s.Username).ToArray(),
						notApproved,
						new Uri("http://" + pullUrl + "pull-requests/" + id),approvedByMe,participating,
						needsAction
						);

					l.Add(x);
				}
			}
			return l;
		}
	}

}
