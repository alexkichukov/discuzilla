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
            app.MapGet("posts", [Authorize] (IPostService _postService, HttpContext context, int? page, int? author, string? search) =>
            {
                int pageSize = 10;
                int p = page ?? 1;
                int a = author ?? -1;
                var posts = _postService.GetAllPosts(context.GetUserID()).Where(p => a == -1 || p.Author.ID == a);


                if (!string.IsNullOrWhiteSpace(search))
                {
                    search = search.ToLower();
                    posts = posts.Where(p => p.Body.ToLower().Contains(search) || p.Title.ToLower().Contains(search) || p.Author.Username.ToLower().Contains(search));
                }

                return new {
                    page = p,
                    totalPages = (posts.Count() + pageSize - 1) / pageSize,
                    posts = posts.Skip(pageSize * (p - 1)).Take(pageSize)
                };
            }).WithTags("Posts");

            // Get a post by id
            app.MapGet("posts/{id:int}", [Authorize] (IPostService _postService, HttpContext context, [FromRoute(Name = "id")] int postID) =>
            {
                var post = _postService.GetPost(postID, context.GetUserID());
                return Results.Ok(post);
            }).WithTags("Posts");

            // Like or unlike a post
            app.MapPut("posts/{id:int}/like", [Authorize] (IPostService _postService, IUserService _userService, HttpContext context, [FromRoute(Name = "id")] int postID) =>
            {
                int userID = context.GetUserID();
                PostDTO post = _postService.GetPost(postID, userID);
                _postService.LikeOrUnlikePost(postID, userID);
                if (post.Author.ID != userID)
                    _userService.AddPoints(post.Author.ID, post.IsLiked ? -1 : 1);
                return Results.Ok();
            }).WithTags("Posts");

            // Create a post
            app.MapPost("posts", [Authorize] (IPostService _postService, HttpContext context, CreatePostDTO postToCreate) =>
            {
                var post = _postService.CreatePost(postToCreate, context.GetUserID());
                return Results.Ok(post);
            }).WithTags("Posts");

            // Delete a post
            app.MapDelete("posts/{id:int}", [Authorize] (IPostService _postService, HttpContext context, [FromRoute(Name = "id")] int postID) =>
            {
                _postService.DeletePost(postID, context.GetUserID());
                return Results.Ok();
            }).WithTags("Posts");

            // Update a post
            app.MapPut("posts/{id:int}", [Authorize] (IPostService _postService, HttpContext context, [FromBody] CreatePostDTO postToUpdate, [FromRoute(Name = "id")] int commentID) =>
            {
                var comment = _postService.UpdatePost(postToUpdate, commentID, context.GetUserID());
                return Results.Ok(comment);
            }).WithTags("Posts");

            // Create a comment
            app.MapPost("comments", [Authorize] (IPostService _postService, HttpContext context, [FromBody] CreateCommentDTO commentToCreate, int post) =>
            {
                var comment = _postService.CreateComment(commentToCreate, post, context.GetUserID());
                return Results.Ok(comment);
            }).WithTags("Comments");

            // Delete a comment
            app.MapDelete("comments/{id:int}", [Authorize] (IPostService _postService, HttpContext context, [FromRoute(Name = "id")] int commentID) =>
            {
                _postService.DeleteComment(commentID, context.GetUserID());
                return Results.Ok();
            }).WithTags("Comments");

            // Update a comment
            app.MapPut("comments/{id:int}", [Authorize] (IPostService _postService, HttpContext context, [FromBody] CreateCommentDTO commentToUpdate, [FromRoute(Name = "id")] int commentID) =>
            {
                var comment = _postService.UpdateComment(commentToUpdate, commentID, context.GetUserID());
                return Results.Ok(comment);
            }).WithTags("Comments");

            // Like or unlike a comment
            app.MapPut("comments/{id:int}/like", [Authorize] (IPostService _postService, IUserService _userService, HttpContext context, [FromRoute(Name = "id")] int commentID) =>
            {
                int userID = context.GetUserID();
                CommentDTO comment = _postService.GetComment(commentID, userID);
                _postService.LikeOrUnlikeComment(commentID, userID);
                if (comment.Author.ID != userID)
                    _userService.AddPoints(comment.Author.ID, comment.IsLiked ? -1 : 1);
                return Results.Ok();
            }).WithTags("Comments");

            // Get a comment by id
            app.MapGet("comments/{id:int}", [Authorize] (IPostService _postService, HttpContext context, [FromRoute(Name = "id")] int commentID) =>
            {
                var post = _postService.GetComment(commentID, context.GetUserID());
                return Results.Ok(post);
            }).WithTags("Comments");

            // Get all comments
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
            }).WithTags("Comments");
        }
    }
}
