using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class DiscuzillaContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;

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
    }
}
