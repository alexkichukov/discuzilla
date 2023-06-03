using Data.Models;

namespace ApplicationService.DTOs
{
    public class SimplifiedUserDTO
    {
        public int ID { get; set; }
        public string Username { get; set; } = null!;

        public SimplifiedUserDTO(User user)
        {
            ID = user.ID;
            Username = user.Username;
        }
    }
}
