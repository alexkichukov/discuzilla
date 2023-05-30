using Data.Models;

namespace ApplicationService.DTOs
{
    public class CreateCommentDTO
    {
        public string Body { get; set; } = null!;

        public bool Validate()
        {
            return !String.IsNullOrEmpty(Body);
        }
    }
}
