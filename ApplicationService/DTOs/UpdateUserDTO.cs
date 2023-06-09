using Data.Models;

namespace ApplicationService.DTOs
{
    public class UpdateUserDTO
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;

        public bool Validate()
        {
            return !String.IsNullOrEmpty(Username) && Username.Length >= 3;
        }
    }
}
