using Data.Models;

namespace ApplicationService.DTOs
{
	public class CommentDTO : BaseDTO
	{
		public string? Body { get; set; }
		public virtual ICollection<UserDTO>? Likes { get; set; }
		public virtual UserDTO? Author { get; set; }
		public virtual PostDTO? Post { get; set; }

		public bool Validate()
		{
			return !String.IsNullOrEmpty(Body);
		}
	}
}
