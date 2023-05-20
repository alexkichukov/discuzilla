using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class BaseEntity
    {
        public int ID { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
