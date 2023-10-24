using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Infra.Contexts;
using BlogPetNews.API.Infra.Utils;
using BlogPetNews.Tests.Common.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace BlogPetNews.Integration.Tests.Util
{
    public class IntegrationTestsMockData
    {
        

        public static async Task<User> Createuser(CustomWebApplicationFactory<Program> _application, User user)
        {
            var cryptography = new Cryptography();

            using (var scope = _application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var petNewsDbContext = provider.GetRequiredService<BlogPetNewsDbContext>())
                {
                    await petNewsDbContext.Database.EnsureCreatedAsync();
                    await petNewsDbContext.Users.AddAsync(user);

                    await petNewsDbContext.SaveChangesAsync();
                    return user;

                }
            }
        }


    }
}
