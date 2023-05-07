using ApplicationService.DTOs;

namespace ApplicationService.Interfaces
{
    public interface IUserService
    {
        public List<UserDTO> GetAll();
        public UserDTO RegisterUser(RegisterUserDTO registerUserDTO);
        public UserDTO Login(LoginUserDTO loginUserDTO);
        public UserDTO GetByID(int ID);
        public bool DeleteUser(int ID);
    }
}
