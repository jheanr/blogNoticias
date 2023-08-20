using BlogPetNews.API.Infra.News;
using BlogPetNews.API.Infra.Users;
using Microsoft.EntityFrameworkCore;

namespace BlogPetNews.API.Infra.Contexts
{
    public class BlogPetNewsDbContext : DbContext
    {
        public BlogPetNewsDbContext(DbContextOptions<BlogPetNewsDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.News.News> News { get; set; }
        public DbSet<Domain.Users.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
            base.OnModelCreating(modelBuilder);

            new NewsEntityConfiguration(modelBuilder.Entity<Domain.News.News>());
            new UserEntityConfiguration(modelBuilder.Entity<Domain.Users.User>());
       }

    }
}
