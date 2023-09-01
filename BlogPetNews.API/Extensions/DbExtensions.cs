using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Infra.Contexts;
using BlogPetNews.API.Infra.News;
using BlogPetNews.API.Infra.Users;
using BlogPetNews.API.Service.News;
using BlogPetNews.API.Service.Users;
using BlogPetNews.API.Service.ViewModels.News;
using BlogPetNews.API.Service.ViewModels.Users;
using FluentValidation;
using Microsoft.EntityFrameworkCore;


namespace BlogPetNews.API.Extensions
{
    public static class DbExtensions
    {
        public static void AddDbServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<BlogPetNewsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            AddRepositories(services);
            AddServices(services);
        }

        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<BlogPetNewsDbContext>();
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

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IValidator<CreateUserDto>, CreateUserDto.CreateUserDtoValidator>();
            services.AddScoped<IValidator<CreateNewsDto>, CreateNewsDto.CreateNewsDtoValidator>();
            services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDto.UpdateUserDtoValidator>();
            services.AddScoped<IValidator<UpdateNewsDto>, UpdateNewsDto.UpdateNewsDtoValidator>();


        }
   
    }
}
