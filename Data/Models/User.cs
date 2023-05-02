using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? Email { get; set; }

        [Column(TypeName = "decimal(6, 1)")]
        public decimal Points { get; set; }
    }
}
