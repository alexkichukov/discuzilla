namespace ApplicationService.DTOs
{
	public class PostDTO : BaseDTO
	{
		public string? Title { get; set; }
		public string? Body { get; set; }
		public virtual ICollection<CommentDTO>? Comments { get; set; }
		public virtual ICollection<UserDTO>? Likes { get; set; }
		public virtual UserDTO? Author { get; set; }

		public bool Validate()
		{
			return !String.IsNullOrEmpty(Title) && !String.IsNullOrEmpty(Body);
		}
	}
}
