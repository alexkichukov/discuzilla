using Data.Models;

namespace ApplicationService.DTOs
{
	public class PostDTO
	{
		public int ID { get; set; }
		public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public SimplifiedUserDTO Author { get; set; }
        public int LikeCount { get; set; }
        public List<CommentDTO> Comments { get; set; } = null!;
        public bool IsLiked { get; set; }
        public DateTime Date { get; set; }

        public bool Validate()
		{
			return !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Body);
		}

        public PostDTO(Post post, int userID)
        {
            ID = post.ID;
            Title = post.Title;
            Body = post.Body;
            Author = new SimplifiedUserDTO(post.User);
            LikeCount = post.Likes.Count;
            Comments = post.Comments.Select(x => new CommentDTO(x, userID)).ToList();
            IsLiked = post.Likes.Where(x => x.UserID == userID).Any();
            Date = post.CreatedOn;
        }
    }
}
