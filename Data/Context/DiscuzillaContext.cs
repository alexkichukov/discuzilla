using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class DiscuzillaContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<PostLike> PostLikes { get; set; } = null!;
        public DbSet<CommentLike> CommentLikes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var local = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(local);

            // Create folder in local AppData
            string localFolder = Path.Join(path, "Discuzilla");
            Directory.CreateDirectory(localFolder);

            string DbPath = Path.Join(localFolder, "discuzilla.db");
            options.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.PostLikes)
                .WithOne(pl => pl.User)
                .HasForeignKey(pl => pl.UserID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CommentLikes)
                .WithOne(cl => cl.User)
                .HasForeignKey(cl => cl.UserID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserID);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Likes)
                .WithOne(pl => pl.Post)
                .HasForeignKey(pl => pl.PostID);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostID);

            modelBuilder.Entity<Comment>()
                .HasMany(c => c.Likes)
                .WithOne(cl => cl.Comment)
                .HasForeignKey(cl => cl.CommentID);
        }
    }
}
