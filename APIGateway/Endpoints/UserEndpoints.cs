using ApplicationService.Interfaces;
using ApplicationService.DTOs;

namespace APIGateway.Endpoints
{
    public static class UserEndpoints
    {
        public static void UseUserEndpoints(this IEndpointRouteBuilder app)
        {
            // GET Users
            app.MapGet("users", (IUserService _userService) => {
                var users = _userService.GetAll();
                return users;
            }).WithName("GetUsers");

            // GET User
            app.MapGet("user/{id:int}", (IUserService _userService, int id) => {
                var user = _userService.GetByID(id);
                return user;
            }).WithName("GetUser");
        }
    }
}
