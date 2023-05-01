using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Comment : BaseEntity
    {
        [Required]
        public string? Body { get; set; }

        public virtual ICollection<User>? Likes { get; set; }

        public virtual User? Author { get; set; }

        public virtual Post? Post { get; set; }
    }
}
