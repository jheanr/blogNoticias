using BlogPetNews.API.Domain.Enums;
using BlogPetNews.API.Domain.UseCases.LoginUser;
using BlogPetNews.API.Infra.Utils;
using BlogPetNews.Integration.Tests.Util;
using BlogPetNews.Tests.Common.Factory;
using BlogPetNews.Tests.Common.News;
using BlogPetNews.Tests.Common.Users;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Xunit.Sdk;

namespace BlogPetNews.Integration.Tests.Post
{
    public class PostNewsTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        private readonly CustomWebApplicationFactory<Program> _application;
        private HttpClient _httpClient;
        private readonly ICryptography _cryptography;

        public PostNewsTest(CustomWebApplicationFactory<Program> application)
        {
            _application = application;
            _httpClient = application.CreateClient();
            _cryptography = new Cryptography();
            _application.AddServiceFake(IntegrationTestHelpers.NewsServiceFake());

        }


        [Fact]
        public async Task Post_News_ShouldReturnSuccess()
        {
            //Arrange
            //var user = UserTestFixture.UserFaker.Generate();
            //var tempPassword = user.Password;
            //user.Password = _cryptography.Encodes(user.Password);
            //user.Role = RolesUser.Admin;

            //await IntegrationTestsMockData.Createuser(_application, user);
            //var httpResponse = await _httpClient.PostAsync($"/login?email={user.Email}&password={tempPassword}", null);

            //var content = await httpResponse.Content.ReadAsStringAsync();

            //var result = JsonConvert.DeserializeObject<LoginUserCommandResponse>(content);

            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + result!.Token);

            //Arrange
            _httpClient = await LoginUser(true);
            var news = NewsTestFixture.CreateNewsDtoFaker.Generate();

            //Act
            var response = await _httpClient.PostAsJsonAsync("/news", news);

            //Asserts
            IntegrationTestHelpers.AssertStatusCodeOk(response);


        }

        [Fact]
        public async Task Post_News_ShouldReturnFailure()
        {
            //Arrange
            _httpClient = await LoginUser(false);
            var news = NewsTestFixture.CreateNewsDtoFaker.Generate();
            news.Title = "";

            //Act
            var response = await _httpClient.PostAsJsonAsync("/news", news);

            //Asserts
            IntegrationTestHelpers.AssertStatusCodeBadRequest(response);
        }

        private async Task<HttpClient> LoginUser(bool userIsAdmin)
        {
            //Arrange
            var user = UserTestFixture.UserFaker.Generate();
            var tempPassword = user.Password;
            user.Password = _cryptography.Encodes(user.Password);
            if(userIsAdmin)
                user.Role = RolesUser.Admin;
            user.Role = RolesUser.User;

            await IntegrationTestsMockData.Createuser(_application, user);
            var httpResponse = await _httpClient.PostAsync($"/login?email={user.Email}&password={tempPassword}", null);

            var content = await httpResponse.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<LoginUserCommandResponse>(content);

            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + result!.Token);

            return _httpClient;
        }

    }
}