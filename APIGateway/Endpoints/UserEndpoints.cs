using ApplicationService.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace APIGateway.Endpoints
{
    public static class UserEndpoints
    {
        public static void UseUserEndpoints(this IEndpointRouteBuilder app)
        {
            // Get all users
            app.MapGet("users", [Authorize] (IUserService _userService) =>
            {
                var users = _userService.GetAll();
                return Results.Ok(users);
            });

            // Get user by id
            app.MapGet("user/{id:int}", [Authorize] (IUserService _userService, int id) =>
            {
                try
                {
                    var user = _userService.GetByID(id);
                    return Results.Ok(user);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            // Get user by username
            app.MapGet("user/{username}", [Authorize] (IUserService _userService, string username) =>
            {
                try
                {
                    var user = _userService.GetByUsername(username);
                    return Results.Ok(user);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });
        }
    }
}
