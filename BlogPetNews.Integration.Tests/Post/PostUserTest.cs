using BlogPetNews.API.Infra.Utils;
using BlogPetNews.Tests.Common.Factory;
using BlogPetNews.Tests.Common.Users;
using BlogPetNews.Tests.Common.Utils;
using System.Net.Http.Json;

namespace BlogPetNews.Integration.Tests.Post
{
    public class PostUserTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _application;
        private HttpClient _httpClient;
        private readonly TestHelpers _testHelpers;
        private readonly ICryptography _cryptography;

        public PostUserTest(CustomWebApplicationFactory<Program> application)
        {
            _application = application;
            _httpClient = application.CreateClient();
            _testHelpers = new TestHelpers(_application);
            _cryptography = new Cryptography();

        }

        [Fact]
        [Trait("Post", "Create a new user")]
        public async Task CreateUser_ShouldReturnSuccess()
        {
            //Arrange
            var user = UserTestFixture.UserFaker.Generate();

            //Act
            var result = await _httpClient.PostAsJsonAsync("/create", user);

            //Assert
            _testHelpers.AssertStatusCodeOk(result);
        }

        [Fact]
        [Trait("Post", "Try create a new user missing a required field")]
        public async Task CreateUser_SouldReturnFailure()
        {
            //Arrange
            var user = UserTestFixture.CreateUserDtoFaker.Generate();
            user.Password = null;

            //Act
            var result = await _httpClient.PostAsJsonAsync("/create", user);

            //Assert
            _testHelpers.AssertStatusCodeBadRequest(result);
        }

        [Fact]
        [Trait("Post", "Login with existent user")]
        public async Task Login_SouldReturnSuccess()
        {
            //Arrange
            var user = UserTestFixture.UserFaker.Generate();
            var tempPassword = user.Password;
            user.Password = _cryptography.Encodes(user.Password);
            await _testHelpers.Createuser(_application, user);

            //Act
            var url = $"/login?email={user.Email}&password={tempPassword}";
            var result = await _httpClient.PostAsync(url, null);

            //Assert
            _testHelpers.AssertStatusCodeOk(result);
        }

        [Fact]
        [Trait("Post", "Login with incorrect fields")]
        public async Task Login_ShouldReturnUnauthorized()
        {
            //Arrange
            var user = UserTestFixture.UserFaker.Generate();
            user.Password = _cryptography.Encodes(user.Password);
            await _testHelpers.Createuser(_application, user);

            //Act
            var url = $"/login?email={user.Email}&password={user.Password}";
            var result = await _httpClient.PostAsync(url, null);

            //Assert
            _testHelpers.AssertStatusCodeUnauthorized(result);
        }


    }
}
