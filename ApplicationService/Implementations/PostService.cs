using ApplicationService.DTOs;
using ApplicationService.Exceptions;
using ApplicationService.Interfaces;
using Data.Models;
using Repository.Implementations;
using System.Net;

namespace ApplicationService.Implementations
{
    public class PostService : IPostService
    {
        public List<SimplifiedPostDTO> GetAllPosts(int userID)
        {
            List<SimplifiedPostDTO> posts = new();

            using (UnitOfWork unitOfWork = new())
            {
                foreach (Post post in unitOfWork.PostRepository.Get(null, null, "User,Likes,Comments"))
                    posts.Add(new(post, userID));
            }

            return posts;
        }

        public PostDTO GetPost(int postID, int userID)
        {
            using UnitOfWork unitOfWork = new();
            Post? post = unitOfWork.PostRepository.Get(p => p.ID == postID, null, "User,Likes,Comments,Comments.Likes").FirstOrDefault();

            if (post == null) throw new ServiceException("No such post found", HttpStatusCode.NotFound);

            return new(post, userID);
        }

        public PostDTO CreatePost(CreatePostDTO postToCreate, int userID)
        {
            if (!postToCreate.Validate()) throw new ServiceException("Invalid data", HttpStatusCode.BadRequest);

            using UnitOfWork unitOfWork = new();
            Post post = new()
            {
                Title = postToCreate.Title,
                Body = postToCreate.Body,
                UserID = userID
            };
            unitOfWork.PostRepository.Insert(post);
            unitOfWork.Save();

            return GetPost(post.ID, userID);
        }

        public PostDTO UpdatePost(CreatePostDTO postToUpdate, int commentID, int userID)
        {
            if (!postToUpdate.Validate()) throw new ServiceException("Invalid data", HttpStatusCode.BadRequest);

            using UnitOfWork unitOfWork = new();
            Post? post = unitOfWork.PostRepository.Get(p => p.ID == commentID).FirstOrDefault();

            if (post == null) throw new ServiceException("No such post found", HttpStatusCode.NotFound);
            else if (post.UserID != userID) throw new ServiceException("Unauthorized to update post", HttpStatusCode.Unauthorized);

            post.Title = postToUpdate.Title;
            post.Body = postToUpdate.Body;
            unitOfWork.PostRepository.Update(post);
            unitOfWork.Save();

            return GetPost(post.ID, post.UserID);
        }

        public void LikeOrUnlikePost(int postID, int userID)
        {
            using UnitOfWork unitOfWork = new();

            // Make sure the post exists first
            if (unitOfWork.PostRepository.Get(x => x.ID == postID).FirstOrDefault() == null)
                throw new ServiceException("No such post found", HttpStatusCode.NotFound);

            PostLike? postLike = unitOfWork.PostLikesRepository.Get(x => x.PostID == postID && x.UserID == userID).FirstOrDefault();

            if (postLike == null) unitOfWork.PostLikesRepository.Insert(new() { PostID = postID, UserID = userID });
            else unitOfWork.PostLikesRepository.Delete(postLike);

            unitOfWork.Save();
        }

        public void DeletePost(int postID, int userID)
        {
            using UnitOfWork unitOfWork = new();
            Post? postToDelete = unitOfWork.PostRepository.Get(p => p.ID == postID).FirstOrDefault();

            if (postToDelete == null) throw new ServiceException("No such post found", HttpStatusCode.NotFound);
            if (postToDelete.UserID != userID) throw new ServiceException("Unauthorized to delete post", HttpStatusCode.Unauthorized);

            unitOfWork.PostRepository.Delete(postToDelete);
            unitOfWork.Save();
        }

        public CommentDTO GetComment(int commentID, int userID)
        {
            using UnitOfWork unitOfWork = new();
            Comment? comment = unitOfWork.CommentRepository.Get(c => c.ID == commentID, null, "Post,User,Likes").FirstOrDefault();

            if (comment == null) throw new ServiceException("No such comment found", HttpStatusCode.NotFound);

            return new(comment, userID);
        }

        public CommentDTO CreateComment(CreateCommentDTO commentToCreate, int postID, int userID)
        {
            if (!commentToCreate.Validate()) throw new ServiceException("Invalid data", HttpStatusCode.BadRequest);

            using UnitOfWork unitOfWork = new();
            Comment comment = new()
            {
                Body = commentToCreate.Body,
                PostID = postID,
                UserID = userID
            };
            unitOfWork.CommentRepository.Insert(comment);
            unitOfWork.Save();

            return GetComment(comment.ID, comment.UserID);
        }

        public CommentDTO UpdateComment(CreateCommentDTO commentToUpdate, int commentID, int userID)
        {
            if (!commentToUpdate.Validate()) throw new ServiceException("Invalid data", HttpStatusCode.BadRequest);

            using UnitOfWork unitOfWork = new();
            Comment? comment = unitOfWork.CommentRepository.Get(c => c.ID == commentID).FirstOrDefault();

            if (comment == null) throw new ServiceException("No such comment found", HttpStatusCode.NotFound);
            else if (comment.UserID != userID) throw new ServiceException("Unauthorized to update comment", HttpStatusCode.Unauthorized);

            comment.Body = commentToUpdate.Body;
            unitOfWork.CommentRepository.Update(comment);
            unitOfWork.Save();

            return GetComment(comment.ID, comment.UserID);
        }

        public void LikeOrUnlikeComment(int commentID, int userID)
        {
            using UnitOfWork unitOfWork = new();

            // Make sure the comment exists first
            if (unitOfWork.CommentRepository.Get(x => x.ID == commentID).FirstOrDefault() == null)
                throw new ServiceException("No such comment found", HttpStatusCode.NotFound);

            CommentLike? commentLike = unitOfWork.CommentLikesRepository.Get(x => x.CommentID == commentID && x.UserID == userID).FirstOrDefault();

            if (commentLike == null) unitOfWork.CommentLikesRepository.Insert(new() { CommentID = commentID, UserID = userID });
            else unitOfWork.CommentLikesRepository.Delete(commentLike);

            unitOfWork.Save();
        }

        public void DeleteComment(int commentID, int userID)
        {
            using UnitOfWork unitOfWork = new();
            Comment? commentToDelete = unitOfWork.CommentRepository.Get(c => c.ID == commentID).FirstOrDefault();

            if (commentToDelete == null) throw new ServiceException("No such comment found", HttpStatusCode.NotFound);
            if (commentToDelete.UserID != userID) throw new ServiceException("Unauthorized to delete comment", HttpStatusCode.Unauthorized);

            unitOfWork.CommentRepository.Delete(commentToDelete);
            unitOfWork.Save();
        }
    }
}
