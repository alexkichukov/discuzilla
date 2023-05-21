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
                else unitOfWork.PostLikesRepository.Delete(postLike.ID);

                unitOfWork.Save();
            }
        }
    }
}
