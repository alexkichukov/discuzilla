using ApplicationService.DTOs;

namespace ApplicationService.Interfaces
{
    public interface IPostService
    {
        public List<SimplifiedPostDTO> GetAll(int userID);
        public PostDTO GetByID(int id, int userID);
        public void LikeOrUnlikePost(int id, int userID);
        public PostDTO CreatePost(CreatePostDTO postToCreate, int userID);
        public void DeletePost(int postID, int userID);
        public CommentDTO CreateComment(CreateCommentDTO commentToCreate, int postID, int userID);
    }
}
