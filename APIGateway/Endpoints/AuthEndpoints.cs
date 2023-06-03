using ApplicationService.DTOs;
using ApplicationService.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIGateway.Endpoints
{
    public static class AuthEndpoints
    {
        public static void UseAuthEndpoints(this IEndpointRouteBuilder app, ConfigurationManager config)
        {
            // Generates a JWT token for a user
            string GenerateToken(UserDTO user)
            {
                byte[] key = Encoding.ASCII.GetBytes(config["Jwt:Key"]);
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                        new Claim("id", user.ID.ToString()),
                        new Claim("username", user.Username),
                        new Claim("email", user.Email ?? "")
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Issuer = config["Jwt:Issuer"],
                    Audience = config["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }

            // Login
            app.MapPost("/login", (IUserService _userService, HttpContext context, LoginUserDTO user) =>
            {
                UserDTO loggedInUser = _userService.Login(user);
                string token = GenerateToken(loggedInUser);
                return Results.Ok(new
                {
                    id = loggedInUser.ID,
                    email = loggedInUser.Email,
                    username = loggedInUser.Username,
                    points = loggedInUser.Points,
                    token
                });
            });

            // Register
            app.MapPost("/register", (IUserService _userService, RegisterUserDTO user) =>
            {
                UserDTO registeredUser = _userService.RegisterUser(user);
                string token = GenerateToken(registeredUser);
                return Results.Ok(new
                {
                    id = registeredUser.ID,
                    email = registeredUser.Email,
                    username = registeredUser.Username,
                    points = registeredUser.Points,
                    token
                });
            });
        }
    }
}
