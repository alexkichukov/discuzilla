using ApplicationService.DTOs;
using ApplicationService.Interfaces;
using Data.Models;
using Repository.Implementations;

namespace ApplicationService.Implementations
{
    public class PostService : IPostService
    {
        public List<SimplifiedPostDTO> GetAll(int userID)
        {
            List<SimplifiedPostDTO> posts = new();

            using (UnitOfWork unitOfWork = new())
            {
                foreach (Post post in unitOfWork.PostRepository.Get())
                    posts.Add(new(post, userID));
            }

            return posts;
        }

        public PostDTO GetByID(int postID, int userID)
        {
            PostDTO postDTO;

            using (UnitOfWork unitOfWork = new())
            {
                Post? post = unitOfWork.PostRepository.Get(p => p.ID == postID).FirstOrDefault();

                if (post == null) throw new Exception("No such post found");

                postDTO = new(post, userID);
            }

            return postDTO;
        }

        public void LikeOrUnlikePost(int postID, int userID)
        {
            using (UnitOfWork unitOfWork = new())
            {
                PostLike? postLike = unitOfWork.PostLikesRepository.Get(x => x.PostID == postID && x.UserID == userID).FirstOrDefault();

                if (postLike == null) unitOfWork.PostLikesRepository.Insert(new() { PostID = postID, UserID = userID });
                else unitOfWork.PostLikesRepository.Delete(postLike);

                unitOfWork.Save();
            }
        }

        public PostDTO CreatePost(CreatePostDTO postToCreate, int userID)
        {
            if (!postToCreate.Validate()) throw new Exception("Invalid data");

            PostDTO postDTO;

            using (UnitOfWork unitOfWork = new())
            {
                // Add new user
                Post post = new()
                {
                    Title = postToCreate.Title,
                    Body = postToCreate.Body,
                    UserID = userID
                };
                unitOfWork.PostRepository.Insert(post);
                unitOfWork.Save();

                postDTO = new(post, userID);
            }

            return postDTO;
        }

        public void DeletePost(int postID, int userID)
        {
            using (UnitOfWork unitOfWork = new())
            {
                Post postToDelete = unitOfWork.PostRepository.GetByID(postID);

                if (postToDelete == null) throw new Exception("No such post found");

                if (postToDelete.UserID != userID) throw new Exception("Unauthorized to delete post");
                
                unitOfWork.PostRepository.Delete(postToDelete);
                unitOfWork.Save();
            }
        }

        public CommentDTO CreateComment(CreateCommentDTO commentToCreate, int postID, int userID)
        {
            if (!commentToCreate.Validate()) throw new Exception("Invalid data");

            CommentDTO commentDTO;

            using (UnitOfWork unitOfWork = new())
            {
                // Add new user
                Comment comment = new()
                {
                    Body = commentToCreate.Body,
                    PostID = postID,
                    UserID = userID
                };
                unitOfWork.CommentRepository.Insert(comment);
                unitOfWork.Save();

                commentDTO = new(comment, userID);
            }

            return commentDTO;
        }
    }
}
