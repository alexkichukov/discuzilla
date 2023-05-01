using ApplicationService.DTOs;
using Data.Models;
using Repository.Implementations;

namespace ApplicationService.Implementations
{
    public class UserManagementService
    {
        public List<DTOs.UserDTO> GetAll()
        {
            List<DTOs.UserDTO> users = new List<DTOs.UserDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (Data.Models.User user in unitOfWork.UserRepository.Get())
                {
                    DTOs.UserDTO userDTO = new DTOs.UserDTO();
                    userDTO.Username = user.Username;
                    userDTO.Points = user.Points;
                    userDTO.Email = user.Email;
                    users.Add(userDTO);
                }
            }

            return users;
        }
    }
}
