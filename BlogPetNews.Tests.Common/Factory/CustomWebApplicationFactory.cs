using BlogPetNews.API.Infra.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlogPetNews.Tests.Common.Factory
{
    public class CustomWebApplicationFactory<Program> : WebApplicationFactory<Program> where Program : class
    {

        private readonly Dictionary<Type, object> _serviceFakes = new Dictionary<Type, object>();

        public void AddServiceFake<TService>(TService service)
        {
            _serviceFakes[typeof(TService)] = service;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices((context, services) =>
            {

                ReplaceServicesWithFakes(services);

                services.RemoveAll(typeof(DbContextOptions<BlogPetNewsDbContext>));
                services.AddDbContext<BlogPetNewsDbContext>(options =>
                {
                    options.UseSqlServer("Server=.\\SQLEXPRESS01;Database=PetNewsDb;Integrated Security=true;TrustServerCertificate=True;");
                });

                base.ConfigureWebHost(builder);
            });
        }

        private void ReplaceServicesWithFakes(IServiceCollection services)
        {
            foreach (var serviceFake in _serviceFakes)
            {
                services.RemoveAll(serviceFake.Key);
                services.AddSingleton(serviceFake.Key, serviceFake.Value);
            }
        }


    }
}
