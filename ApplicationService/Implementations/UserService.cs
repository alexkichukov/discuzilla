using ApplicationService.DTOs;
using ApplicationService.Exceptions;
using ApplicationService.Interfaces;
using Data.Models;
using Repository.Implementations;
using System.ComponentModel.Design;
using System.Net;
using BC = BCrypt.Net.BCrypt;

namespace ApplicationService.Implementations
{
    public class UserService : IUserService
    {
        public List<UserDTO> GetAll()
        {
            List<UserDTO> users = new();

            using UnitOfWork unitOfWork = new();
            foreach (User user in unitOfWork.UserRepository.Get()) users.Add(new(user));

            return users;
        }

        public UserDTO RegisterUser(RegisterUserDTO registerUserDTO)
        {
            if (!registerUserDTO.Validate()) throw new ServiceException("Invalid data", HttpStatusCode.BadRequest);

            using UnitOfWork unitOfWork = new();

            // Check if user with username exists already
            User? alreadyRegisteredUser = unitOfWork.UserRepository.Get(u => u.Username == registerUserDTO.Username).FirstOrDefault();
            if (alreadyRegisteredUser != null) throw new ServiceException("User already registered", HttpStatusCode.Conflict);

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

            return new(user);
        }

        public UserDTO Login(LoginUserDTO loginUserDTO)
        {
            if (!loginUserDTO.Validate()) throw new ServiceException("Invalid data", HttpStatusCode.BadRequest);

            using UnitOfWork unitOfWork = new();
            
            User? user = unitOfWork.UserRepository.Get(user => user.Username == loginUserDTO.Username).FirstOrDefault();

            if (user == null) throw new ServiceException("No such user found", HttpStatusCode.NotFound);

            if (BC.Verify(loginUserDTO.Password, user.Password)) return new(user);
            else throw new ServiceException("Incorrect password", HttpStatusCode.Unauthorized);
        }

        public UserDTO GetByID(int userID)
        {
            using UnitOfWork unitOfWork = new();
            User? user = unitOfWork.UserRepository.Get(u => u.ID == userID).FirstOrDefault();

            if (user == null) throw new ServiceException("No such user found", HttpStatusCode.NotFound);

            return new(user);
        }

        public void UpdateUser(UpdateUserDTO updateUserDTO, int userID)
        {
            if (!updateUserDTO.Validate()) throw new ServiceException("Invalid data", HttpStatusCode.BadRequest);

            using UnitOfWork unitOfWork = new();
            User? user = unitOfWork.UserRepository.Get(u => u.ID == userID).FirstOrDefault();

            if (user == null) throw new ServiceException("No such user found", HttpStatusCode.NotFound);

            user.Username = updateUserDTO.Username;
            user.Email = updateUserDTO.Email;
            unitOfWork.UserRepository.Update(user);
            unitOfWork.Save();
        }

        public bool DeleteUser(int userID)
        {
            using UnitOfWork unitOfWork = new();
            User? userToDelete = unitOfWork.UserRepository.Get(u => u.ID == userID, include: "Posts,Comments,PostLikes,CommentLikes").FirstOrDefault();

            Console.WriteLine("hello");

            if (userToDelete == null) return false;

            Console.WriteLine("test");

            unitOfWork.UserRepository.Delete(userToDelete);
            unitOfWork.Save();

            return true;
        }

        public void AddPoints(int userID, decimal points)
        {
            using UnitOfWork unitOfWork = new();
            User? user = unitOfWork.UserRepository.Get(u => u.ID == userID).FirstOrDefault();

            if (user == null) throw new ServiceException("No such user found", HttpStatusCode.NotFound);

            user.Points += points;
            unitOfWork.UserRepository.Update(user);
            unitOfWork.Save();
        }
    }
}
