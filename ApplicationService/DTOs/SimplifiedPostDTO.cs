using Data.Models;

namespace ApplicationService.DTOs
{
    public class SimplifiedPostDTO
    {
        public int ID { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public SimplifiedUserDTO Author { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public bool IsLiked { get; set; }
        public DateTime Date { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Body);
        }

        public SimplifiedPostDTO(Post post, int userID)
        {
            ID = post.ID;
            Title = post.Title;
            Body = post.Body.Length > 100 ? $"{post.Body[..100]}..." : post.Body;
            Author = new SimplifiedUserDTO(post.User);
            LikeCount = post.Likes.Count;
            CommentCount = post.Comments.Count;
            IsLiked = post.Likes.Where(x => x.UserID == userID).Any();
            Date = post.CreatedOn;
        }
    }
}
