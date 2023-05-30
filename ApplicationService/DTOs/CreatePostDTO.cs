using Data.Models;

namespace ApplicationService.DTOs
{
    public class CreatePostDTO
    {
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;

        public bool Validate()
        {
            return !String.IsNullOrEmpty(Title) && !String.IsNullOrEmpty(Body);
        }
    }
}