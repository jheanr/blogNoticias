using BlogPetNews.Integration.Tests.Util;
using BlogPetNews.Tests.Common.Factory;
using BlogPetNews.Tests.Common.News;
using System.Net.Http.Json;

namespace BlogPetNews.Integration.Tests.Post
{
    public class PostNewsTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _application;
        private HttpClient _httpClient;
        private readonly TestHelpers _testHelpers;


        public PostNewsTest(CustomWebApplicationFactory<Program> application)
        {
            _application = application;
            _httpClient = application.CreateClient();
            _testHelpers = new TestHelpers(_application);

        }

        [Fact]
        [Trait("Post", "Create a new news with success")]
        public async Task Post_News_ShouldReturnSuccess()
        {
            //Arrange
            _httpClient = await _testHelpers.LoginUser(true);
            var news = NewsTestFixture.CreateNewsDtoFaker.Generate();

            //Act
            var response = await _httpClient.PostAsJsonAsync("/news", news);

            //Asserts
            _testHelpers.AssertStatusCodeOk(response);

        }

        [Fact]
        [Trait("Post", "Try create a new missing a required field")]
        public async Task Post_News_ShouldReturnFailure()
        {
            //Arrange
            _httpClient = await _testHelpers.LoginUser(false);
            var news = NewsTestFixture.CreateNewsDtoFaker.Generate();
            news.Title = "";

            //Act
            var response = await _httpClient.PostAsJsonAsync("/news", news);

            //Asserts
            _testHelpers.AssertStatusCodeBadRequest(response);

        }

    }
}