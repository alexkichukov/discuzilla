using ApplicationService.Implementations;

UserManagementService userService = new UserManagementService();

var users = userService.GetAll();

foreach (var user in users)
{
    Console.WriteLine($"User {user.ID}:");
    Console.WriteLine($"User {user.Email}:");
}