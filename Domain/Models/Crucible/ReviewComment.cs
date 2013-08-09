namespace Domain.Models.Crucible
{
	using System.ComponentModel.DataAnnotations;

	public class ReviewComment
	{

		[Key]
		public string Id { get; private set; }

		public string User { get; private set; }

		public string Message { get; private set; }
		
		public string ReadStatus { get; private set; }

		public string ReviewId { get; set; }

		public string ReviewItemId { get; set; }

		public bool Draft { get; private set; }

		public ReviewComment(string readStatus, string message, string id, bool draft, string user,string reviewId, string reviewItemId)
		{
			
			ReadStatus = readStatus;
			Message = message;
			Id = id;
			Draft = draft;
			User = user;
			ReviewId = reviewId;
			ReviewItemId = reviewItemId;
		}

	}
}
