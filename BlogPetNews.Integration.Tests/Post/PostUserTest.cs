using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Service.ViewModels.News;
using BlogPetNews.Integration.Tests.Util;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http.Json;

namespace BlogPetNews.Integration.Tests.Post
{
    public class PostUserTest
    {
        [Fact]
        public async Task POST_CreateUserReturnOk()
        {
            await using var application = new PetNewsApiApplication();

            var user = new User
            {
                Name = "El Gato",
                Email = "elgato@miau.net",
                Password = "gato@321"
            };

            var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/create", user);

            IntegrationTestHelpers.AssertStatusCodeOk(result);
        }

        [Fact]
        public async Task POST_CreateUserReturnFailure()
        {
            await using var application = new PetNewsApiApplication();

            var user = new User
            {
                Name = "El Gato",
                Email = "elgato@miau.net",
            };

            var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/create", user);

            IntegrationTestHelpers.AssertStatusCodeBadRequest(result);
        }

        [Fact]
        public async Task POST_LoginSuccess()
        {
            await using var application = new PetNewsApiApplication();

            await PetNewsMockData.CreateUser(application, true);

            var user = new User { Email = "elgato@miau.net", Password = "gato@123" };

            var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/login", user);

            IntegrationTestHelpers.AssertStatusCodeOk(result);

        }

        [Fact]
        public async Task POST_LoginUnauthorized()
        {
            await using var application = new PetNewsApiApplication();

            await PetNewsMockData.CreateUser(application, true);

            var user = new User
            {
                Email = "admin@admin.com",
                Password = "SenhaSecreta"
            };

            var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/login", user);

            IntegrationTestHelpers.AssertStatusCodeUnauthorized(result);
        }
    }
}
