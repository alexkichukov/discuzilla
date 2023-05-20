using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Comment : BaseEntity
    {
        [Required]
        public string Body { get; set; } = null!;
        public int PostID { get; set; }
        public virtual Post Post { get; set; } = null!;
        public int UserID { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<CommentLike> Likes { get; set; } = null!;

        public Comment() : base()
        {
            Likes = new List<CommentLike>();
        }
    }

    public class CommentLike : BaseEntity
    {
        public int CommentID { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Comment Comment { get; set; } = null!;
    }
}
