using ApplicationService.DTOs;
using ApplicationService.Interfaces;
using Data.Models;
using Repository.Implementations;
using BC = BCrypt.Net.BCrypt;

namespace ApplicationService.Implementations
{
    public class UserService : IUserService
    {
        public List<UserDTO> GetAll()
        {
            List<UserDTO> users = new();

            using (UnitOfWork unitOfWork = new())
            {
                foreach (User user in unitOfWork.UserRepository.Get())
                {
                    UserDTO userDTO = new();
                    userDTO.ID = user.ID;
                    userDTO.Username = user.Username;
                    userDTO.Points = user.Points;
                    userDTO.Email = user.Email;
                    users.Add(userDTO);
                }
            }

            return users;
        }

        public UserDTO RegisterUser(RegisterUserDTO registerUserDTO)
        {
            if (!registerUserDTO.Validate()) throw new Exception("Invalid DTO");

            UserDTO userDTO = new();

            using (UnitOfWork unitOfWork = new())
            {
                // Check if user with username or email exists
                User? alreadyRegisteredUser = unitOfWork.UserRepository.Get(u => u.Email == registerUserDTO.Email || u.Username == registerUserDTO.Username).FirstOrDefault();
                if (alreadyRegisteredUser != null)
                {
                    throw new Exception("User already registered.");
                }

                // Add new user
                User user = new();
                user.Username = registerUserDTO.Username;
                user.Email = registerUserDTO.Email;
                user.Points = 0;
                user.Password = BC.HashPassword(registerUserDTO.Password);
                unitOfWork.UserRepository.Insert(user);
                unitOfWork.Save();

                userDTO.ID = user.ID;
                userDTO.Username = user.Username;
                userDTO.Email = user.Email;
                userDTO.Points = user.Points;
            }

            return userDTO;
        }

        public UserDTO Login(LoginUserDTO loginUserDTO)
        {
            if (!loginUserDTO.Validate()) throw new Exception("Invalid DTO");

            UserDTO userDTO = new();

            using (UnitOfWork unitOfWork = new())
            {
                User? user = unitOfWork.UserRepository.Get(user => user.Username == loginUserDTO.Username).FirstOrDefault();

                if (user == null) throw new Exception("No such user found");

                if (BC.Verify(loginUserDTO.Password, user.Password))
                {
                    userDTO.ID = user.ID;
                    userDTO.Username = user.Username;
                    userDTO.Email = user.Email;
                    userDTO.Points = user.Points;
                }
                else
                {
                    throw new Exception("Incorrect password");
                }
            }

            return userDTO;
        }

        public UserDTO GetByID(int ID)
        {
            UserDTO userDTO = new();

            using (UnitOfWork unitOfWork = new())
            {
                User? user = unitOfWork.UserRepository.GetByID(ID);

                if (user == null) throw new Exception("No such user found");

                userDTO.ID = user.ID;
                userDTO.Username = user.Username;
                userDTO.Email = user.Email;
                userDTO.Points = user.Points;
            }

            return userDTO;
        }

        public bool DeleteUser(int ID)
        {
            using (UnitOfWork unitOfWork = new())
            {
                User userToDelete = unitOfWork.UserRepository.GetByID(ID);

                if (userToDelete == null) return false;

                unitOfWork.UserRepository.Delete(ID);
                unitOfWork.Save();
            }

            return true;
        }
    }
}
