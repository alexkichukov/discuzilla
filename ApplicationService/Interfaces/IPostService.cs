using ApplicationService.DTOs;

namespace ApplicationService.Interfaces
{
    public interface IPostService
    {
        // Posts
        public List<SimplifiedPostDTO> GetAllPosts(int userID);
        public PostDTO GetPost(int id, int userID);
        public PostDTO CreatePost(CreatePostDTO postToCreate, int userID);
        public PostDTO UpdatePost(CreatePostDTO postToUpdate, int commentID, int userID);
        public void LikeOrUnlikePost(int id, int userID);
        public void DeletePost(int postID, int userID);

        // Comments
        public CommentDTO GetComment(int commentID, int userID);
        public CommentDTO CreateComment(CreateCommentDTO commentToCreate, int postID, int userID);
        public CommentDTO UpdateComment(CreateCommentDTO commentToUpdate, int commentID, int userID);
        public void LikeOrUnlikeComment(int commentID, int userID);
        public void DeleteComment(int commentID, int userID);
    }
}
