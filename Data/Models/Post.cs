using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Post : BaseEntity
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Body { get; set; } = null!;
        public int UserID { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; } = null!;
        public virtual ICollection<PostLike> Likes { get; set; } = null!;

        public Post() : base()
        {
            Comments = new List<Comment>();
            Likes = new List<PostLike>();
        }
    }

    public class PostLike : BaseEntity
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; } = null!;
        public Post Post { get; set; } = null!;
    }
}
