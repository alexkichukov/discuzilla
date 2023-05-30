using APIGateway.Extensions;
using ApplicationService.DTOs;
using ApplicationService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            app.MapGet("post/{id:int}", [Authorize] (IPostService _postService, HttpContext context, [FromRoute(Name = "id")] int postID) =>
            {
                try
                {
                    var post = _postService.GetByID(postID, context.GetUserID());
                    return Results.Ok(post);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            // Like or unlike a post
            app.MapGet("post/{id:int}/like", [Authorize] (IPostService _postService, HttpContext context, [FromRoute(Name = "id")] int postID) =>
            {
                try
                {
                    var post = _postService.GetByID(postID, context.GetUserID());
                    _postService.LikeOrUnlikePost(post.ID, context.GetUserID());
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            // Create a post
            app.MapPost("post", [Authorize] (IPostService _postService, HttpContext context, CreatePostDTO postToCreate) =>
            {
                try
                {
                    var post = _postService.CreatePost(postToCreate, context.GetUserID());
                    return Results.Ok(post);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            // Delete a post
            app.MapDelete("post/{id:int}", [Authorize] (IPostService _postService, HttpContext context, [FromRoute(Name = "id")] int postID) =>
            {
                try
                {
                    _postService.DeletePost(postID, context.GetUserID());
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex.Message);
                }
            });

            // Create a comment
            app.MapPost("post/{id:int}/comment", [Authorize] (IPostService _postService, HttpContext context, [FromBody] CreateCommentDTO commentToCreate, [FromRoute(Name = "id")] int postID) =>
            {
                try
                {
                    var comment = _postService.CreateComment(commentToCreate, postID, context.GetUserID());
                    return Results.Ok(comment);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });
        }
    }
}
