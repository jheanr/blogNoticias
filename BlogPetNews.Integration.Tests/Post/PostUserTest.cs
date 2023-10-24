using BlogPetNews.API.Infra.Utils;
using BlogPetNews.Integration.Tests.Util;
using BlogPetNews.Tests.Common.Factory;
using BlogPetNews.Tests.Common.Users;
using System.Net.Http.Json;

namespace BlogPetNews.Integration.Tests.Post
{
    public class PostUserTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _application;
        private readonly HttpClient _httpClient;
        private readonly ICryptography _cryptography;

        public PostUserTest(CustomWebApplicationFactory<Program> application)
        {
            _application = application;
            _httpClient = application.CreateClient();
            _cryptography = new Cryptography();
            _application.AddServiceFake(IntegrationTestHelpers.UserServiceFake());
        }

        [Fact]
        public async Task CreateUser_ShouldReturnSuccess()
        {
            //Arrange
            var user = UserTestFixture.UserFaker.Generate();

            //Act
            var result = await _httpClient.PostAsJsonAsync("/create", user);

            //Assert
            IntegrationTestHelpers.AssertStatusCodeOk(result);
        }

        [Fact]
        public async Task CreateUser_SouldReturnFailure()
        {
            //Arrange
            var user = UserTestFixture.CreateUserDtoFaker.Generate();
            user.Password = null;

            //Act
            var result = await _httpClient.PostAsJsonAsync("/create", user);

            //Assert
            IntegrationTestHelpers.AssertStatusCodeBadRequest(result);
        }

        [Fact]
        public async Task Login_SouldReturnSuccess()
        {
            //Arrange
            var user = UserTestFixture.UserFaker.Generate();
            var tempPassword = user.Password;
            user.Password = _cryptography.Encodes(user.Password);
            await IntegrationTestsMockData.Createuser(_application, user);

            //Act
            var url = $"/login?email={user.Email}&password={tempPassword}";
            var result = await _httpClient.PostAsync(url, null);

            //Assert
            IntegrationTestHelpers.AssertStatusCodeOk(result);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized()
        {
            //Arrange
            var user = UserTestFixture.UserFaker.Generate();
            user.Password = _cryptography.Encodes(user.Password);
            await IntegrationTestsMockData.Createuser(_application, user);

            //Act
            var url = $"/login?email={user.Email}&password={user.Password}";
            var result = await _httpClient.PostAsync(url, null);

            //Assert
            IntegrationTestHelpers.AssertStatusCodeUnauthorized(result);
        }


    }
}
