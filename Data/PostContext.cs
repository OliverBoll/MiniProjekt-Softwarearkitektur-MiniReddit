using Microsoft.EntityFrameworkCore;
using MiniProject.Models;

namespace MiniProject.Data
{

    public class PostContext : DbContext
    {
        public DbSet<Post> Posts => Set<Post>();

        public DbSet<User> Users => Set<User>();

        public PostContext(DbContextOptions<PostContext> options) : base(options)
        {

        }

    }
}