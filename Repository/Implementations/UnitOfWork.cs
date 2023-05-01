using Data.Context;
using Data.Models;

namespace Repository.Implementations
{
    public class UnitOfWork : IDisposable
    {
        private DiscuzillaContext context = new DiscuzillaContext();
        private GenericRepository<User> userRepository;
        private GenericRepository<Post> postRepository;
        private GenericRepository<Comment> commentRepository;

        public GenericRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }
        public GenericRepository<Post> PostRepository
        {
            get
            {

                if (this.postRepository == null)
                {
                    this.postRepository = new GenericRepository<Post>(context);
                }
                return postRepository;
            }
        }
        public GenericRepository<Comment> CommentRepository
        {
            get
            {

                if (this.commentRepository == null)
                {
                    this.commentRepository = new GenericRepository<Comment>(context);
                }
                return commentRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
