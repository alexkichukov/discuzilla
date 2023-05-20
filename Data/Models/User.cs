using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Column(TypeName = "decimal(6, 1)")]
        public decimal Points { get; set; }
        public string Email { get; set; } = null!;
        public virtual ICollection<Post> Posts { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; } = null!;
        public virtual ICollection<PostLike> PostLikes { get; set; } = null!;
        public virtual ICollection<CommentLike> CommentLikes { get; set; } = null!;

        public User() : base()
        {
            Posts = new List<Post>();
            Comments = new List<Comment>();
            PostLikes = new List<PostLike>();
            CommentLikes = new List<CommentLike>();
        }
    }
}
