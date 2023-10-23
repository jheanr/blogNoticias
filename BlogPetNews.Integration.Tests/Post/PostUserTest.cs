using BlogPetNews.API.Service.ViewModels.Users;
using BlogPetNews.Integration.Tests.Util;
using System.Net.Http.Json;

namespace BlogPetNews.Integration.Tests.Post
{
    public class PostUserTest : IClassFixture<PetNewsApiApplication>
    {
        private readonly PetNewsApiApplication _application;
        private readonly HttpClient _httpClient;

        public PostUserTest(PetNewsApiApplication application)
        {
            _application = application;
            _httpClient = application.CreateClient();
        }

        [Fact]
        public async Task POST_CreateUserReturnOk()
        {
            var user = new CreateUserDto
            {
                Name = "El Gato",
                Email = "elgato@miau.net",
                Password = "gato@321"
            };

            var result = await _httpClient.PostAsJsonAsync("/create", user);

            IntegrationTestHelpers.AssertStatusCodeOk(result);
        }

        [Fact]
        public async Task POST_CreateUserReturnFailure()
        {
            var user = new CreateUserDto
            {
                Name = "El Gato",
                Email = "elgato@miau.net",
            };

            var result = await _httpClient.PostAsJsonAsync("/create", user);

            IntegrationTestHelpers.AssertStatusCodeBadRequest(result);
        }

        [Fact]
        public async Task POST_LoginSuccess()
        {
            await PetNewsMockData.CreateUser(_application, true);

            var user = new 
            { 
                Email = "elgato@miau.net", 
                Password = "gato@123"
            };

            var url = $"/login?email={user.Email}&password={user.Password}";
            var result = await _httpClient.PostAsync(url, null);

            IntegrationTestHelpers.AssertStatusCodeOk(result);

        }

        [Fact]
        public async Task POST_LoginUnauthorized()
        {
            await PetNewsMockData.CreateUser(_application, true);

            var user = new
            {
                Email = "admin@admin.com",
                Password = "SenhaSecreta"
            };

            var url = $"/login?email={user.Email}&password={user.Password}";
            var result = await _httpClient.PostAsync(url, null);

            IntegrationTestHelpers.AssertStatusCodeUnauthorized(result);
        }
    }
}
