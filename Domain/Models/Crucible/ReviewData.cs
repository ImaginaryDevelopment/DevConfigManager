namespace Domain.Models.Crucible
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Domain.Extensions;

	public class ReviewData
	{
		
		readonly string _id;

		readonly string _name;

		readonly string _description;

		readonly string _author;

		readonly string _creator;

		readonly string _created;

		readonly string _projectKey;

		readonly string _state;

		readonly string _dueDate;

		readonly Uri _link;

		[System.ComponentModel.DataAnnotations.Key]
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		public IEnumerable<string> IncompleteReviewers { get; private set; }

		public int UnreadComments { get; set; }

		public bool IsCompletedByMe { get; private set; }

		public bool Participating { get; private set; }

		public string Name
		{
			get
			{
				return this._name;
			}
		}

		public Uri Link
		{
			get
			{
				return this._link;
			}
		}

		public string DueDate
		{
			get
			{
				return this._dueDate;
			}
		}

		public string Description
		{
			get
			{
				return this._description;
			}
		}

		public string Author
		{
			get
			{
				return this._author;
			}
		}

		public string Creator
		{
			get
			{
				return this._creator;
			}
		}

		public string Created
		{
			get
			{
				return this._created;
			}
		}

		public int TotalComments { get; set; }

		public IEnumerable<string> CompletedReviewers { get; private set; }

		public string ProjectKey
		{
			get
			{
				return this._projectKey;
			}
		}

		public string State
		{
			get
			{
				return this._state;
			}
		}

		public ReviewData(
			string id,
			string name,
			string description,
			string author,
			string creator,
			string created,
			string projectKey,
			string state,
			string dueDate,
			Uri link,
			IEnumerable<string> completedReviewers,
			IEnumerable<string> incompleteReviewers,
			int unreadComments,
			int totalComments,
			string currentUser)
		{
			var completed = completedReviewers.ToArray();
			this.CompletedReviewers = completed;
			this.IncompleteReviewers = incompleteReviewers;
			UnreadComments = unreadComments;
			TotalComments = totalComments;
			this.IsCompletedByMe = completed.Contains(currentUser, StringComparer.CurrentCultureIgnoreCase);
			this.Participating = completed.Contains(currentUser, StringComparer.CurrentCultureIgnoreCase) || this.IncompleteReviewers.Contains(currentUser, StringComparer.CurrentCultureIgnoreCase);
			this._id = id;
			this._name = name;
			this._description = description.Contains("{cs:id=") ? description.After("}") : description;
			this._author = author;
			this._creator = creator;
			this._created = created;
			this._projectKey = projectKey;
			this._state = state;
			this._dueDate = dueDate;
			this._link = link;
		}
	}
}