namespace ApplicationService.DTOs
{
    public class RegisterUserDTO
    {
        public string? Username { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }

		public bool Validate()
		{
			return !String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Email);
		}
	}
}
