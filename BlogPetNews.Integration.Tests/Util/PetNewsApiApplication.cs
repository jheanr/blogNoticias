using BlogPetNews.API.Infra.Contexts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace BlogPetNews.Integration.Tests.Util
{
    public class PetNewsApiApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<BlogPetNewsDbContext>))

                services.AddDbContext<BlogPetNewsDbContext>(options =>
                    options.UseInMemoryDatabase("BlogPetNewsDb", root));
            });

            return base.CreateHost(builder);
        }

    }
}
