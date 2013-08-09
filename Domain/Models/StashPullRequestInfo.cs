namespace Domain.Models
{
	using System;
	using System.Collections.Generic;

	public class StashPullRequestInfo
	{

		readonly string _id;

		readonly string _author;

		readonly int _completeCount;

		readonly string _title;

		readonly string _description;

		readonly string _state;

		readonly Uri _link;

		readonly bool _approvedByMe;

		public string Id
		{
			get
			{
				return this._id;
			}
		}

		public string Author
		{
			get
			{
				return this._author;
			}
		}

		public int CompleteCount
		{
			get
			{
				return this._completeCount;
			}
		}

		public string Title
		{
			get
			{
				return this._title;
			}
		}

		public string Description
		{
			get
			{
				return this._description;
			}
		}

		public string State
		{
			get
			{
				return this._state;
			}
		}

		public Uri Link
		{
			get
			{
				return this._link;
			}
		}

		public bool ApprovedByMe
		{
			get
			{
				return this._approvedByMe;
			}
		}

		public IEnumerable<string> SuggestedReviewers { get; private set; }

		public IEnumerable<string> RemainingReviewers { get; private set; }

		public bool Participating { get; private set; }

		public bool NeedsAction { get; private set; }

		public StashPullRequestInfo(
			string id,
			string author,
			int completeCount,
			string title,
			string description,
			string state,
			IEnumerable<string> suggestedReviewers,
			IEnumerable<string> remainingReviewers,
			Uri link,
			bool approvedByMe,bool participating,
						bool needsAction)
		{
			this.SuggestedReviewers = suggestedReviewers;
			this.RemainingReviewers = remainingReviewers;
			Participating = participating;
			NeedsAction = needsAction;
			this._id = id;
			this._author = author;
			this._completeCount = completeCount;
			this._title = title;
			this._description = description;
			this._state = state;
			this._link = link;
			this._approvedByMe = approvedByMe;
		}
	}
}