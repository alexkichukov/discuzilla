using ApplicationService.Interfaces;
using ApplicationService.DTOs;

namespace APIGateway.Endpoints
{
    public static class UserEndpoints
    {
        public static void UseUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("users", (IUserService _userService) =>
            {
                var users = _userService.GetAll();
                return users;
            });
        }
    }
}
