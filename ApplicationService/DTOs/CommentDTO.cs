using Data.Models;

namespace ApplicationService.DTOs
{
	public class CommentDTO
	{
		public int ID { get; set; }
		public int UserID { get; set; }
		public int PostID { get; set; }
        public string Body { get; set; } = null!;
		public List<int> Likes { get; set; } = null!;
        public bool IsLiked { get; set; }

		public bool Validate()
		{
			return !String.IsNullOrEmpty(Body);
		}

        public CommentDTO(Comment comment, int userID)
        {
            ID = comment.ID;
            Body = comment.Body;
            UserID = comment.UserID;
            PostID = comment.PostID;
            Likes = comment.Likes.Select(x => x.UserID).ToList();
            IsLiked = comment.Likes.Where(x => x.UserID == userID).Any();
        }
    }
}
