using Data.Models;

namespace ApplicationService.DTOs
{
    public class SimplifiedPostDTO
    {
        public int ID { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public int UserID { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public bool IsLiked { get; set; }

        public bool Validate()
        {
            return !String.IsNullOrEmpty(Title) && !String.IsNullOrEmpty(Body);
        }

        public SimplifiedPostDTO(Post post, int userID)
        {
            ID = post.ID;
            Title = post.Title;
            Body = post.Body[..(post.Body.Length > 100 ? 100 : post.Body.Length - 1)];
            UserID = post.UserID;
            LikeCount = post.Likes.Count;
            CommentCount = post.Comments.Count;
            IsLiked = post.Likes.Where(x => x.UserID == userID).Any();
        }
    }
}
