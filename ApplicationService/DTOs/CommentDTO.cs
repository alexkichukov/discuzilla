using Data.Models;

namespace ApplicationService.DTOs
{
	public class CommentDTO
	{
		public int ID { get; set; }
		public int AuthorID { get; set; }
		public int PostID { get; set; }
		public string? Body { get; set; }
		public virtual List<int> Likes { get; set; } = null!;

		public bool Validate()
		{
			return !String.IsNullOrEmpty(Body);
		}
	}
}
