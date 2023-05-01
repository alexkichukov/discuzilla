using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Post : BaseEntity
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Body { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }

        public virtual ICollection<User>? Likes { get; set; }

        public virtual User? Author { get; set; }
    }
}
