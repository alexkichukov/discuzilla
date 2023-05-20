namespace ApplicationService.DTOs
{
	public class LoginUserDTO
	{
		public string Username { get; set; } = null!;
		public string Password { get; set; } = null!;

		public bool Validate()
		{
			return !String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password);
		}
	}
}
