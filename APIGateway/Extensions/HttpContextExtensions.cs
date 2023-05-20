using System.Security.Claims;

namespace APIGateway.Extensions
{
    public static class HttpContextExtensions
    {
        // Returns the username of the current authorized user
        public static string GetUser(this HttpContext context)
        {
            ClaimsIdentity? user = context.User.Identity as ClaimsIdentity;

            if (user == null || user.FindFirst("username") == null) throw new Exception("Invalid user");
            
            return user.FindFirst("username")!.Value;
        }
    }
}
