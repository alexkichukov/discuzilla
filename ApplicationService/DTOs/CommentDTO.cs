using Data.Models;

namespace ApplicationService.DTOs
{
	public class CommentDTO
	{
		public int ID { get; set; }
        public string Body { get; set; } = null!;
        public SimplifiedUserDTO Author { get; set; }
        public SimplifiedPostDTO Post { get; set; }
		public int LikeCount { get; set; }
        public bool IsLiked { get; set; }
        public DateTime Date { get; set; }

        public bool Validate()
		{
			return !string.IsNullOrEmpty(Body);
		}

        public CommentDTO(Comment comment, int userID)
        {
            ID = comment.ID;
            Body = comment.Body;
            Author = new SimplifiedUserDTO(comment.User);
            Post = new SimplifiedPostDTO(comment.Post, comment.UserID);
            LikeCount = comment.Likes.Count;
            IsLiked = comment.Likes.Where(x => x.UserID == userID).Any();
            Date = comment.CreatedOn;
        }
    }
}
