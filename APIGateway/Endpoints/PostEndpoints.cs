using APIGateway.Extensions;
using ApplicationService.Implementations;
using ApplicationService.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace APIGateway.Endpoints
{
    public static class PostEndpoints
    {
        public static void UsePostEndpoints(this IEndpointRouteBuilder app)
        {
            // Get all posts
            app.MapGet("posts", [Authorize] (IPostService _postService, HttpContext context) =>
            {
                var posts = _postService.GetAll(context.GetUserID());
                return posts;
            });

            // Get a post by id
            app.MapGet("post/{id:int}", [Authorize] (IPostService _postService, HttpContext context, int id) =>
            {
                try
                {
                    var post = _postService.GetByID(id, context.GetUserID());
                    return Results.Ok(post);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            // Like or unlike a post
            app.MapGet("post/{id:int}/like", [Authorize] (IPostService _postService, HttpContext context, int id) =>
            {
                try
                {
                    var post = _postService.GetByID(id, context.GetUserID());
                    _postService.LikeOrUnlikePost(post.ID, context.GetUserID());
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });
        }
    }
}
