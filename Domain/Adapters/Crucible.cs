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
	using Domain.Models.Crucible;

	public class Crucible
	{
		readonly ILog _logger;

		readonly ICanDownload _downloader;

		public Crucible(ILog logger, ICanDownload downloader)
		{
			_logger = logger;
			_downloader = downloader;
		}

		async Task<IDictionary<string, dynamic>> GetReviewInJson(string authority, string urlSuffix)
		{
			var url = "http://" + authority + "/rest-service/reviews-v1/";
			var fullUrl = url + urlSuffix;
			var rawJson = await _downloader.Download(new Uri(fullUrl), "application/json");
			_logger.Log(JsonPrettifier.PrettyPrint( rawJson)+Environment.NewLine);
			
			var formatted = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, dynamic>>(rawJson);
			return formatted;
		}

		public async Task<IEnumerable<ReviewComment>> GetReviewComments(string authority, string permaId)
		{
			var commentsInJson = await GetReviewInJson(authority, permaId + "/comments");
			var result = new List<ReviewComment>();
			
			foreach (dynamic c in commentsInJson["comments"])
			{
				if ((bool)c.deleted)
				{
					continue;
				}
				string readStatus = c.readStatus;
				string message = c.message;
				string commentId;
				string reviewItemId;
				try
				{
					reviewItemId = c.reviewItemId.id;
				}
				catch (Exception)
				{
					reviewItemId = string.Empty;
				}
				try
				{
					commentId = c.permaId.id;
				}
				catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
				{
					commentId = c.permaId.ToString();
				}
				
				bool draft = c.draft;
				string userName = c.user.userName;
				var comment = new ReviewComment(readStatus, message, commentId, draft, userName, permaId, reviewItemId);
				result.Add(comment);
			}

			return result;
		}

		public async Task<IEnumerable<ReviewReviewer>> GetReviewReviewers(string authority, string permaId)
		{
			var reviewersResponse = await GetReviewInJson(authority, permaId + "/reviewers");
			var reviewers = new List<ReviewReviewer>();
			foreach (dynamic r in reviewersResponse["reviewer"])
			{
				string displayName = r.displayName;
				string userName = r.userName;
				bool completed = r.completed;
				string avatarUrl = r.avatarUrl;
				ulong? timeSpent = r.timeSpent;

				var reviewer = new ReviewReviewer(displayName, userName, completed, new Uri(avatarUrl), timeSpent);
				reviewers.Add(reviewer);
			}
			return reviewers;
		}

		public async Task<IEnumerable<ReviewData>> GetReviews(string currentUser, string authority, string filter = null)
		{
			var l = new List<ReviewData>();
			IComparer<ReviewData> comparer = null;
			var filterSuffix = filter == null ? string.Empty : ("filter/" + filter);
			var response = await GetReviewInJson(authority, filterSuffix);

			bool first = true;

			foreach (dynamic v in response["reviewData"])
			{
				var id = (string)v.permaId.id;

				var reviewers = await this.GetReviewReviewers(authority, id);
				var comments = await this.GetReviewComments(authority, id);
				ReviewData item = CreateReviewData(authority, v, currentUser, reviewers, comments);
				
				if (first)
				{
					comparer = item.CreateComparer((left, right) => string.CompareOrdinal(left.Id, right.Id));
					first = false;
				}

				l.Add(item);
			}

			if (l.Count > 0)
				l.Sort(comparer);
			return l.ToArray();
		}

		private ReviewData CreateReviewData(
			string authority,
			dynamic v,
			string currentUser,
			IEnumerable<ReviewReviewer> reviewers,
			IEnumerable<ReviewComment> comments)
		{
			string id = v.permaId.id;
			string name = v.name;
			string description = v.description;
			string authorName = v.author.userName;
			string creatorName = v.creator.userName;
			string createDate = v.createDate;
			string projectKey = v.projectKey;
			string state = v.state;
			string dueDate = v.dueDate;
			var uri = new Uri("http://" + authority + "/cru/" + id);
			var x = new ReviewData(
				id,
				name,
				description,
				authorName,
				creatorName,
				createDate,
				projectKey,
				state,
				dueDate,
				uri,
				reviewers.Where(r => r.Completed).Select(r => r.UserName).ToArray(),
					reviewers.Where(r => !r.Completed).Select(r => r.UserName).ToArray(),
					comments.Count(r => r.ReadStatus != "READ"),
					comments.Count(),
					currentUser);
			return x;
		}

		public async Task<IEnumerable<ReviewData>> GetOpenReviews(string currentUser, string authority)
		{
			
			return await this.GetReviews(currentUser, authority, "allOpenReviews");
		}
	}
}
