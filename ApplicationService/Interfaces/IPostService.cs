using ApplicationService.DTOs;

namespace ApplicationService.Interfaces
{
    public interface IPostService
    {
        public List<SimplifiedPostDTO> GetAll(int userID);
        public PostDTO GetByID(int id, int userID);
        public void LikeOrUnlikePost(int id, int userID);
    }
}
