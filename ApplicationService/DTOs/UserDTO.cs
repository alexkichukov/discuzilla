namespace ApplicationService.DTOs
{
	public class UserDTO
	{
		public int ID { get; set; }
		public string? Username { get; set; }
		public string? Email { get; set; }
		public decimal Points { get; set; }

		public bool Validate()
		{
			return !String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Email);
		}
	}
}
