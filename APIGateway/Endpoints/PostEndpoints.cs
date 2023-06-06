using APIGateway.Extensions;
using ApplicationService.DTOs;
using ApplicationService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace APIGateway.Endpoints
{
    public static class PostEndpoints
    {
        public static void UsePostEndpoints(this IEndpointRouteBuilder app)
        {
            // Get all posts
            app.MapGet("posts", [Authorize] (IPostService _postService, HttpContext context, int? page, int? author) =>
            {
                int pageSize = 10;
                int p = page ?? 1;
                int a = author ?? -1;
                var posts = _postService.GetAllPosts(context.GetUserID()).Where(p => a == -1 || p.Author.ID == a);

                return new {
                    page = p,
                    totalPages = (posts.Count() + pageSize - 1) / pageSize,
                    posts = posts.Skip(pageSize * (p - 1)).Take(pageSize)
                };
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
            app.MapPost("comments", [Authorize] (IPostService _postService, HttpContext context, [FromBody] CreateCommentDTO commentToCreate, int post) =>
            {
                var comment = _postService.CreateComment(commentToCreate, post, context.GetUserID());
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

            // Like or unlike a comment
            app.MapPut("comments/{id:int}/like", [Authorize] (IPostService _postService, HttpContext context, [FromRoute(Name = "id")] int commentID) =>
            {
                _postService.LikeOrUnlikeComment(commentID, context.GetUserID());
                return Results.Ok();
            });

            // Get a comment by id
            app.MapGet("comments/{id:int}", [Authorize] (IPostService _postService, HttpContext context, [FromRoute(Name = "id")] int commentID) =>
            {
                var post = _postService.GetComment(commentID, context.GetUserID());
                return Results.Ok(post);
            });

            // Get all posts
            app.MapGet("comments", [Authorize] (IPostService _postService, HttpContext context, int? page, int author) =>
            {
                int pageSize = 10;
                int p = page ?? 1;
                var comments = _postService.GetAllComments(context.GetUserID()).Where(c => c.Author.ID == author);

                return new
                {
                    page = p,
                    totalPages = (comments.Count() + pageSize - 1) / pageSize,
                    comments = comments.Skip(pageSize * (p - 1)).Take(pageSize)
                };
            });
        }
    }
}
