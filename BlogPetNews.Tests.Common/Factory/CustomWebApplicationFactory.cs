using BlogPetNews.API.Infra.Utils;
using BlogPetNews.API.Service.News;
using BlogPetNews.API.Service.Users;
using BlogPetNews.Tests.Common.News;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            builder.ConfigureServices((context, services) =>
            {

                ReplaceServicesWithMocks(services);

            });
        }

        private void ReplaceServicesWithMocks(IServiceCollection services)
        {
            foreach (var serviceFake in _serviceFakes)
            {
                services.RemoveAll(serviceFake.Key);
                services.AddSingleton(serviceFake.Key, serviceFake.Value);
            }
        }


    }
}
