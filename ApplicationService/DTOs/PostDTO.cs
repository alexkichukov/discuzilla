using Data.Models;

namespace ApplicationService.DTOs
{
	public class PostDTO
	{
		public int ID { get; set; }
		public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
		public int UserID { get; set; }
        public List<int> Likes { get; set; } = null!;
        public List<CommentDTO> Comments { get; set; } = null!;
        public bool IsLiked { get; set; }

        public bool Validate()
		{
			return !String.IsNullOrEmpty(Title) && !String.IsNullOrEmpty(Body);
		}

        public PostDTO(Post post, int userID)
        {
            ID = post.ID;
            Title = post.Title;
            Body = post.Body;
            UserID = post.UserID;
            Likes = post.Likes.Select(x => x.UserID).ToList();
            Comments = post.Comments.Select(x => new CommentDTO(x)).ToList();
            IsLiked = post.Likes.Where(x => x.UserID == userID).Any();
        }
    }
}
