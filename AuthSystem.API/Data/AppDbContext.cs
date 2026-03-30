using AuthSystem.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthSystem.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Unique index for User
            modelBuilder.Entity<User>().HasIndex(u  => u.Username).IsUnique();

            //Default value for createdAt
            modelBuilder.Entity<User>().Property(u => u.CreateAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
