using BlogPetNews.API.Infra.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;
using Xunit;

namespace BlogPetNews.Tests.Common.Factory
{
    public class CustomWebApplicationFactory<Program> : WebApplicationFactory<Program>, IAsyncLifetime where Program : class
    {
        private readonly Dictionary<Type, object> _serviceFakes = new Dictionary<Type, object>();
        private readonly MsSqlContainer _sqlServerContainer = new MsSqlBuilder().Build();

        public void AddServiceFake<TService>(TService service)
        {
            _serviceFakes[typeof(TService)] = service;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                ReplaceServicesWithFakes(services);

                services.RemoveAll(typeof(DbContextOptions<BlogPetNewsDbContext>));
                services.AddDbContext<BlogPetNewsDbContext>(options =>
                {
                    options.UseSqlServer(_sqlServerContainer.GetConnectionString());
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

        public async Task InitializeAsync()
        {
            await _sqlServerContainer.StartAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        { 
            await _sqlServerContainer.StopAsync();
        }
    }
}
