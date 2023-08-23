using BlogPetNews.API.Domain.Users;
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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
            modelBuilder.ApplyConfiguration(new NewsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

            base.OnModelCreating(modelBuilder);
       }
    }
}
