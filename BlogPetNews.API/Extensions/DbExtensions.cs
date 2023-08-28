using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Infra.Contexts;
using BlogPetNews.API.Infra.News;
using BlogPetNews.API.Infra.Users;
using Microsoft.EntityFrameworkCore;

namespace BlogPetNews.API.Extensions
{
    public static class DbExtensions
    {
        public static void AddDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<BlogPetNewsDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            AddRepositories(services);
        }

        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<BlogPetNewsDbContext>();
                    context.Database.EnsureCreated();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or initializing the database.");
                }
            }

            return app;
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
