using BlogPetNews.API.Domain.Enums;
using BlogPetNews.API.Domain.UseCases.CreateNews;
using BlogPetNews.API.Domain.UseCases.LoginUser;
using BlogPetNews.API.Infra.Utils;
using BlogPetNews.API.Service.ViewModels.News;
using BlogPetNews.Integration.Tests.Util;
using BlogPetNews.Tests.Common.Factory;
using BlogPetNews.Tests.Common.News;
using BlogPetNews.Tests.Common.Users;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlogPetNews.Integration.Tests.Delete
{
    public class DeleteNewsTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _application;
        private HttpClient _httpClient;
        private readonly ICryptography _cryptography;
        public DeleteNewsTest(CustomWebApplicationFactory<Program> application)
        {
            _application = application;
            _httpClient = application.CreateClient();
            _cryptography = new Cryptography();
        }

        [Fact]
        [Trait("Delete", "Delete news")]
        public async Task Delete_News_ShouldReturnSuccess()
        {
            //Arrange
            _httpClient = await LoginUser(true);
            var news = await CreateNews(_httpClient);

            //Act
            var response = await _httpClient.DeleteAsync($"/news/{news.Id}");

            //Asserts
            IntegrationTestHelpers.AssertStatusCodeOk(response);
        }


        [Fact]
        [Trait("Delete", "Delete non-existent news")]
        public async Task Delete_News_ShouldReturnBadRequest()
        {
            //Arrange
            _httpClient = await LoginUser(true);
            Guid id = Guid.NewGuid();

            //Act
            var response = await _httpClient.DeleteAsync($"/news/{id}");

            //Asserts
            IntegrationTestHelpers.AssertStatusCodeNotFound(response);
        }

        [Fact]
        [Trait("Delete", "Delete news unauthorized")]
        public async Task Delete_News_ByNonAdminUser_ShouldReturnForbidden()
        {
            //Arrange
            _httpClient = await LoginUser(false);
            var news = await CreateNews(_httpClient);

            //Act
            var response = await _httpClient.DeleteAsync($"/news/{news.Id}");

            //Asserts
            IntegrationTestHelpers.AssertStatusCodeForbidden(response);
        }



        private async Task<ReadNewsDto> CreateNews(HttpClient client)
        {
            var news = NewsTestFixture.CreateNewsDtoFaker.Generate();
            var response = await _httpClient.PostAsJsonAsync("/news", news);

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CreateNewsCommandResponse>(content);

            IntegrationTestHelpers.AssertStatusCodeOk(response);

            return result!.News;

        }

        private async Task<HttpClient> LoginUser(bool userIsAdmin)
        {
            //Arrange
            var user = UserTestFixture.UserFaker.Generate();
            var tempPassword = user.Password;
            user.Password = _cryptography.Encodes(user.Password);

            if (userIsAdmin)
                user.Role = RolesUser.Admin;
            else
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