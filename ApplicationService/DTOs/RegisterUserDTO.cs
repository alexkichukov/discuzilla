namespace ApplicationService.DTOs
{
    public class RegisterUserDTO
    {
		public string Username { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string? Email { get; set; }

		public bool Validate()
		{
			return !String.IsNullOrEmpty(Username);
		}
	}
}
