using ApplicationService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            app.MapGet("user/{id:int}", [Authorize] (IUserService _userService, [FromRoute(Name = "id")] int userID) =>
            {
                var user = _userService.GetByID(userID);
                return Results.Ok(user);
            });

            // Get user by username
            app.MapGet("user/{username}", [Authorize] (IUserService _userService, string username) =>
            {
                var user = _userService.GetByUsername(username);
                return Results.Ok(user);
            });
        }
    }
}
