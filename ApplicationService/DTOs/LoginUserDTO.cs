namespace ApplicationService.DTOs
{
	public class LoginUserDTO
	{
		public string? Username { get; set; }
		public string? Password { get; set; }

		public bool Validate()
		{
			return !String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password);
		}
	}
}
