namespace ApplicationService.DTOs
{
	public class PostDTO
	{
		public int ID { get; set; }
		public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
		public int AuthorID { get; set; }
        public List<int> Likes { get; set; } = null!;
        public virtual List<CommentDTO> Comments { get; set; } = null!;

        public bool Validate()
		{
			return !String.IsNullOrEmpty(Title) && !String.IsNullOrEmpty(Body);
		}
	}
}
