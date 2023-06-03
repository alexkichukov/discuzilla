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
                var posts = _postService.GetAllPosts(context.GetUserID());
                return posts;
            });

            // Get a post by id
            app.MapGet("posts/{id:int}", [Authorize] (IPostService _postService, HttpContext context, [FromRoute(Name = "id")] int postID) =>
            {
                var post = _postService.GetPost(postID, context.GetUserID());
                return Results.Ok(post);
            });

            // Like or unlike a post
            app.MapPut("posts/{id:int}/like", [Authorize] (IPostService _postService, HttpContext context, [FromRoute(Name = "id")] int postID) =>
            {
                _postService.LikeOrUnlikePost(postID, context.GetUserID());
                return Results.Ok();
            });

            // Create a post
            app.MapPost("posts", [Authorize] (IPostService _postService, HttpContext context, CreatePostDTO postToCreate) =>
            {
                var post = _postService.CreatePost(postToCreate, context.GetUserID());
                return Results.Ok(post);
            });

            // Delete a post
            app.MapDelete("posts/{id:int}", [Authorize] (IPostService _postService, HttpContext context, [FromRoute(Name = "id")] int postID) =>
            {
                _postService.DeletePost(postID, context.GetUserID());
                return Results.Ok();
            });

            // Update a post
            app.MapPut("posts/{id:int}", [Authorize] (IPostService _postService, HttpContext context, [FromBody] CreatePostDTO postToUpdate, [FromRoute(Name = "id")] int commentID) =>
            {
                var comment = _postService.UpdatePost(postToUpdate, commentID, context.GetUserID());
                return Results.Ok(comment);
            });

            // Create a comment
            app.MapPost("posts/{id:int}/comment", [Authorize] (IPostService _postService, HttpContext context, [FromBody] CreateCommentDTO commentToCreate, [FromRoute(Name = "id")] int postID) =>
            {
                var comment = _postService.CreateComment(commentToCreate, postID, context.GetUserID());
                return Results.Ok(comment);
            });

            // Delete a comment
            app.MapDelete("comments/{id:int}", [Authorize] (IPostService _postService, HttpContext context, [FromRoute(Name = "id")] int commentID) =>
            {
                _postService.DeleteComment(commentID, context.GetUserID());
                return Results.Ok();
            });

            // Update a comment
            app.MapPut("comments/{id:int}", [Authorize] (IPostService _postService, HttpContext context, [FromBody] CreateCommentDTO commentToUpdate, [FromRoute(Name = "id")] int commentID) =>
            {
                var comment = _postService.UpdateComment(commentToUpdate, commentID, context.GetUserID());
                return Results.Ok(comment);
            });

            // Like or unlike a post
            app.MapPut("comments/{id:int}/like", [Authorize] (IPostService _postService, HttpContext context, [FromRoute(Name = "id")] int commentID) =>
            {
                _postService.LikeOrUnlikeComment(commentID, context.GetUserID());
                return Results.Ok();
            });

            // Get a post by id
            app.MapGet("comments/{id:int}", [Authorize] (IPostService _postService, HttpContext context, [FromRoute(Name = "id")] int commentID) =>
            {
                var post = _postService.GetComment(commentID, context.GetUserID());
                return Results.Ok(post);
            });
        }
    }
}
