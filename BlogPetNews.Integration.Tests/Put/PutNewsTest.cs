using BlogPetNews.API.Service.ViewModels.News;
using BlogPetNews.Tests.Common.Factory;
using BlogPetNews.Tests.Common.News;
using BlogPetNews.Tests.Common.Utils;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BlogPetNews.Integration.Tests.Put
{
    public class PutNewsTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        private readonly CustomWebApplicationFactory<Program> _application;
        private HttpClient _httpClient;
        private readonly TestHelpers _testHelpers;

        public PutNewsTest(CustomWebApplicationFactory<Program> application)
        {
            _application = application;
            _httpClient = application.CreateClient();
            _testHelpers = new TestHelpers(_application);

        }


        [Fact]
        [Trait("Put", "Success update a new")]
        public async Task Put_Update_A_New_Should_Return_Success()
        {
            //Arrange

            _httpClient = await _testHelpers.LoginUser(true);

            var news = await _testHelpers.CreateNews(_httpClient);
            var getResponse = await _httpClient.GetAsync($"/news/{news.Id}");
            _testHelpers.AssertStatusCodeOk(getResponse);

            var getContent = await getResponse.Content.ReadAsStringAsync();
            var returnedGetNews = JsonConvert.DeserializeObject<ReadNewsDto>(getContent);
            returnedGetNews!.Title = "Updated Title";

            //Act
            var response = await _httpClient.PutAsJsonAsync($"/news/{returnedGetNews.Id}", returnedGetNews);


            //Asserts
            _testHelpers.AssertStatusCodeOk(response);

        }


        [Fact]
        [Trait("Put", "Try to update a non-existent new")]
        public async Task Put_Update_A_New_Should_Return_Failure()
        {
            //Arrange

            _httpClient = await _testHelpers.LoginUser(true);

            var returnedGetNews = NewsTestFixture.ReadNewsDtoFaker.Generate();
            returnedGetNews!.Title = "Updated Title";

            //Act
            var response = await _httpClient.PutAsJsonAsync($"/news/{returnedGetNews.Id}", returnedGetNews);

            //Asserts
            _testHelpers.AssertStatusCodeNotFound(response);

        }

        [Fact]
        [Trait("Put", "Try to update a new with not authorized user")]
        public async Task Put_Update_A_New_Should_Return_NotAuthorized()
        {

            var news = NewsTestFixture.ReadNewsDtoFaker.Generate();

            //Act
            var response = await _httpClient.PutAsJsonAsync($"/news/{news.Id}", news);


            //Asserts
            _testHelpers.AssertStatusCodeUnauthorized(response);

        }
    }
}