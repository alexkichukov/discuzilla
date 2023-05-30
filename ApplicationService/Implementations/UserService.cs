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
                    users.Add(new(user));
            }

            return users;
        }

        public UserDTO RegisterUser(RegisterUserDTO registerUserDTO)
        {
            if (!registerUserDTO.Validate()) throw new Exception("Invalid data");

            UserDTO userDTO;

            using (UnitOfWork unitOfWork = new())
            {
                // Check if user with username or email exists
                User? alreadyRegisteredUser = unitOfWork.UserRepository.Get(u => u.Email == registerUserDTO.Email || u.Username == registerUserDTO.Username).FirstOrDefault();
                if (alreadyRegisteredUser != null)
                {
                    throw new Exception("User already registered");
                }

                // Add new user
                User user = new()
                {
                    Username = registerUserDTO.Username,
                    Email = registerUserDTO.Email ?? "",
                    Points = 0,
                    Password = BC.HashPassword(registerUserDTO.Password)
                };
                unitOfWork.UserRepository.Insert(user);
                unitOfWork.Save();

                // Create DTO from user
                userDTO = new(user);
            }

            return userDTO;
        }

        public UserDTO Login(LoginUserDTO loginUserDTO)
        {
            if (!loginUserDTO.Validate()) throw new Exception("Invalid DTO");

            UserDTO userDTO;

            using (UnitOfWork unitOfWork = new())
            {
                User? user = unitOfWork.UserRepository.Get(user => user.Username == loginUserDTO.Username).FirstOrDefault();

                if (user == null) throw new Exception("No such user found");

                if (BC.Verify(loginUserDTO.Password, user.Password)) userDTO = new(user);
                else throw new Exception("Incorrect password");
            }

            return userDTO;
        }

        public UserDTO GetByID(int userID)
        {
            UserDTO userDTO;

            using (UnitOfWork unitOfWork = new())
            {
                User? user = unitOfWork.UserRepository.Get(u => u.ID == userID).FirstOrDefault();

                if (user == null) throw new Exception("No such user found");

                userDTO = new(user);
            }

            return userDTO;
        }

        public UserDTO GetByUsername(string username)
        {
            UserDTO userDTO;

            using (UnitOfWork unitOfWork = new())
            {
                User? user = unitOfWork.UserRepository.Get(u => u.Username == username).FirstOrDefault();

                if (user == null) throw new Exception("No such user found");

                userDTO = new(user);
            }

            return userDTO;
        }

        public bool DeleteUser(int userID)
        {
            using (UnitOfWork unitOfWork = new())
            {
                User userToDelete = unitOfWork.UserRepository.GetByID(userID);

                if (userToDelete == null) return false;

                unitOfWork.UserRepository.Delete(userToDelete);
                unitOfWork.Save();
            }

            return true;
        }
    }
}
