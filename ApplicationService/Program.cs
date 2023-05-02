using ApplicationService.Implementations;
using ApplicationService.DTOs;
using BC = BCrypt.Net.BCrypt;

// Test stuff
UserManagementService userService = new UserManagementService();

//var users = userService.GetAll();

//foreach (var user in users)
//{
//    Console.WriteLine($"User {user.ID}:");
//    Console.WriteLine($"User {user.Email}:");
//}

 userService.Login(new LoginUserDTO { Username = "alex.k", Password = "12345" });

// Console.WriteLine(userService.DeleteUser(1));

//userService.RegisterUser(new RegisterUserDTO { Email = "alex.kichukovv@gmail.com", Password = "12345", Username = "alex.k" });