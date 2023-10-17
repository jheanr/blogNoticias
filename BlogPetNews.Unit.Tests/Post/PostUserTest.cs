using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Service.ViewModels.News;
using BlogPetNews.Unit.Tests.Utils;
using System.Net.Http.Json;

namespace BlogPetNews.Unit.Tests.Post
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

            UnitHelper.AssertStatusCodeOk(result);
        }

        [Fact]
        public async Task POST_CreateUserReturnFailure()
        {
            await using var application = new PetNewsApiApplication();

            var user = new User
            {
                Email = "elgato@miau.net",
            };

            var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/create", user);

            UnitHelper.AssertStatusCodeBadRequest(result);
        }

        [Fact]
        public async Task POST_LoginSuccess()
        {
            await using var application = new PetNewsApiApplication();

            await PetNewsMockData.CreateUser(application, true);

            var user = new User { Email = "elgato@miau.net",Password = "gato@123"};

            var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/login", user);

            UnitHelper.AssertStatusCodeOk(result);

        }

        [Fact]
        public async Task POST_LoginUnauthorized()
        {
            await using var application = new PetNewsApiApplication();
            
            await PetNewsMockData.CreateUser(application, true);

            var user = new User
            {
                Email = "admin@admin.com",
                Password = "pass123"
            };

            var client = application.CreateClient();
            var result = await client.PostAsJsonAsync("/login", user);
            
            UnitHelper.AssertStatusCodeUnauthorizedd(result);
        }

        [Fact]
        public async Task POST_CreateNewsReturnsNothAuthorized()
        {
            await using var application = new PetNewsApiApplication();

            await PetNewsMockData.CreateNews(application,true);

            var newsDto = new CreateNewsDto
            {
                Title = "Capriche no telhado",
                Content = "Namoro bom é quando você deixa os humanos sem conseguir dormir"
            };


            var client = application.CreateClient();
            var response = await client.PostAsJsonAsync("/news/create", newsDto);

            var news = await client.GetFromJsonAsync<List<News>>("/news");

            UnitHelper.AssertStatusCodeUnauthorizedd(response);
            Assert.True(news.Count >= 2);
        }
    }
}