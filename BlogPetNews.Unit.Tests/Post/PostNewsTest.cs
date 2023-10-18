using BlogPetNews.API.Domain.News;
using BlogPetNews.Unit.Tests.Utils;
using System.Net.Http.Json;

namespace BlogPetNews.Unit.Tests.Post
{
    public class PostNewsTest
    {
        [Fact]
        public async Task POST_CreateNewsReturnsUnauthorized()
        {
            await using var application = new PetNewsApiApplication();

            await PetNewsMockData.CreateNews(application, true);

            var news = new News
            {
                Title = "Capriche no telhado",
                Content = "Namoro bom é quando você deixa os humanos sem conseguir dormir"
            };


            var client = application.CreateClient();
            var response = await client.PostAsJsonAsync("/news", news);

            UnitHelper.AssertStatusCodeUnauthorized(response);
        }

        [Fact]
        public async Task POST_CreateNewsReturnsOk()
        {
          
        }
    }
}