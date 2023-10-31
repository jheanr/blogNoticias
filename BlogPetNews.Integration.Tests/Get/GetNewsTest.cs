using BlogPetNews.API.Service.ViewModels.News;
using BlogPetNews.Tests.Common.Factory;
using BlogPetNews.Tests.Common.Utils;
using Newtonsoft.Json;

namespace BlogPetNews.Integration.Tests.Get
{
    public class GetNewsTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        private readonly CustomWebApplicationFactory<Program> _application;
        private HttpClient _httpClient;
        private readonly TestHelpers _testHelpers;

        public GetNewsTest(CustomWebApplicationFactory<Program> application)
        {
            _application = application;
            _httpClient = application.CreateClient();
            _testHelpers = new TestHelpers(_application);
        }


        [Fact]
        [Trait("Get", "Get all created news")]
        public async Task Get_News_ShouldReturnAllNews()
        {
            //Arrange
            _httpClient = await _testHelpers.LoginUser(true);
            var newsList = new List<ReadNewsDto>();

            for (int i = 0; i < 3; i++)
            {
                newsList.Add(await _testHelpers.CreateNews(_httpClient));
            }

            //Act
            var response = await _httpClient.GetAsync("/news");
            var content = await response.Content.ReadAsStringAsync();
            var news = JsonConvert.DeserializeObject<List<ReadNewsDto>>(content);

            //Asserts
            Assert.NotNull(news);
            Assert.True(news.Count >= 3);
            _testHelpers.AssertStatusCodeOk(response);

        }

        [Fact]
        [Trait("Get", "Get news with a scpecifc ID")]

        public async Task Get_News_ShouldReturnNewsById()
        {
            //Arrange

            _httpClient = await _testHelpers.LoginUser(true);
            
             var news = await _testHelpers.CreateNews(_httpClient);

            //Act
            var response = await _httpClient.GetAsync($"/news/{news.Id}");
            var content = await response.Content.ReadAsStringAsync();
            var returnedNews = JsonConvert.DeserializeObject<ReadNewsDto>(content);

            //Asserts
            _testHelpers.AssertStatusCodeOk(response);
            Assert.Equal(returnedNews!.Id, news.Id);
        }

        [Fact]
        [Trait("Get", "Get a non-existent new")]
        public async Task Get_News_ShouldReturnNotFound()
        {
            
            //Act
            var response = await _httpClient.GetAsync($"/news/{Guid.NewGuid()}");

            //Asserts
            _testHelpers.AssertStatusCodeNotFound(response);
        }
    }
}