using APIGateway.Extensions;
using ApplicationService.DTOs;
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

            // User leaderboard
            app.MapGet("leaderboard", [Authorize] (IUserService _userService, int? page) =>
            {
                int pageSize = 10;
                int p = page ?? 1;
                var users = _userService.GetAll().OrderByDescending(u => u.Points);

                return new
                {
                    page = p,
                    totalPages = (users.Count() + pageSize - 1) / pageSize,
                    users = users.Skip(pageSize * (p - 1)).Take(pageSize)
                };
            });

            // Update user
            app.MapPut("user", [Authorize] (IUserService _userService, HttpContext context, [FromBody] UpdateUserDTO updateUser) =>
            {
                _userService.UpdateUser(updateUser, context.GetUserID());
                return Results.Ok();
            });
        }
    }
}
