using BlogPetNews.Tests.Common.Factory;
using BlogPetNews.Tests.Common.Util;

namespace BlogPetNews.Integration.Tests.Delete
{
    public class DeleteNewsTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _application;
        private HttpClient _httpClient;
        private readonly TestHelpers _testHelpers;

        public DeleteNewsTest(CustomWebApplicationFactory<Program> application)
        {
            _application = application;
            _httpClient = application.CreateClient();
            _testHelpers = new TestHelpers(_application);
        }

        [Fact]
        [Trait("Delete", "Delete news")]
        public async Task Delete_News_ShouldReturnSuccess()
        {
            //Arrange
            _httpClient = await _testHelpers.LoginUser(true);
            var news = await _testHelpers.CreateNews(_httpClient);

            //Act
            var response = await _httpClient.DeleteAsync($"/news/{news.Id}");

            //Asserts
            _testHelpers.AssertStatusCodeOk(response);
        }


        [Fact]
        [Trait("Delete", "Delete non-existent news")]
        public async Task Delete_News_ShouldReturnBadRequest()
        {
            //Arrange
            _httpClient = await _testHelpers.LoginUser(true);
            Guid id = Guid.NewGuid();

            //Act
            var response = await _httpClient.DeleteAsync($"/news/{id}");

            //Asserts
            _testHelpers.AssertStatusCodeNotFound(response);
        }

        [Fact]
        [Trait("Delete", "Delete news unauthorized")]
        public async Task Delete_News_ByNonAdminUser_ShouldReturnForbidden()
        {
            //Arrange
            _httpClient = await _testHelpers.LoginUser(false);
            var news = await _testHelpers.CreateNews(_httpClient);

            //Act
            var response = await _httpClient.DeleteAsync($"/news/{news.Id}");

            //Asserts
            _testHelpers.AssertStatusCodeForbidden(response);
        }

    }
}