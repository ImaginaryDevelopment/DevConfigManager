namespace Domain.Models.Crucible
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class ReviewReviewer
	{
		public ReviewReviewer(string displayName, string userName, bool completed, Uri avatarUrl, ulong? timeSpent)
		{
			this.AvatarUrl = avatarUrl;
			TimeSpent = timeSpent ??0;
			this.Completed = completed;
			this.UserName = userName;
			this.DisplayName = displayName;
		}

		public string DisplayName { get; private set; }

		[Key]
		public string UserName { get; private set; }

		public bool Completed { get; private set; }

		public Uri AvatarUrl { get; private set; }
		
		public ulong TimeSpent { get; set; }
	}
}
