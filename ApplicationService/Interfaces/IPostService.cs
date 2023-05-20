using ApplicationService.DTOs;

namespace ApplicationService.Interfaces
{
    interface IPostService
    {
        public List<PostDTO> GetAll();
    }
}
