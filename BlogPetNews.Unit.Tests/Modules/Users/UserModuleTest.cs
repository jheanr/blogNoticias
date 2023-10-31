using BlogPetNews.API.Domain.UseCases.LoginUser;
using BlogPetNews.API.Service.Users;
using BlogPetNews.API.Service.ViewModels.Users;
using BlogPetNews.Tests.Common.Util;
using BlogPetNews.Tests.Common.Factory;
using BlogPetNews.Tests.Common.Users;
using Newtonsoft.Json;
using NSubstitute;
using System.Net.Http.Json;

namespace BlogPetNews.Unit.Tests.Modules.Users
{
    public class UserModuleTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _application;
        private readonly HttpClient _httpClient;
        private readonly TestHelpers _testHelpers;

        public UserModuleTest(CustomWebApplicationFactory<Program> application)
        {
            _application = application;

            _application.AddServiceFake(ServicesFakes());
            _httpClient = _application.CreateClient();

            _testHelpers = new TestHelpers(_application);
        }

        [Fact]
        public async Task Login_User_ShouldReturnSuccess()
        {

            //Arrange
            var login = new { Email = "test@test.com", Password = "123456" };

            //Act
            HttpResponseMessage response = await _httpClient.PostAsync($"/login?email={login.Email}&password={login.Password}", null);

            //Assert
            _testHelpers.AssertStatusCodeOk(response);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginUserCommandResponse>(content);

            Assert.DoesNotContain(result!.Token, "Unauthorized Access");
        
        }

        [Trait("Create", "Validate Create User")]
        [Fact]
        public async Task Create_User_ShouldReturnSuccess()
        {

            //Arrange
            var user = UserTestFixture.CreateUserDtoFaker.Generate();

            //Act
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/create/", user);

            //Assert
            _testHelpers.AssertStatusCodeOk(response);

        }


        [Trait("Create", "Validate Create Invalid User")]
        [Fact]
        public async Task Create_User_ShouldReturnInvalid()
        {

            //Arrange
            var user = UserTestFixture.CreateUserDtoFaker.Generate();
            user.Email = "test";
            user.Password = "";

            //Act
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/create/", user);

            //Assert
            _testHelpers.AssertStatusCodeBadRequest(response);

        }



        private static IUserService ServicesFakes()
        {
            #region ServiceFake
            var userServiceFake = Substitute.For<IUserService>();

            //Create
            userServiceFake.Create(Arg.Any<CreateUserDto>()).Returns(UserTestFixture.ReadUserDtoFaker.Generate());

            //Login
            userServiceFake.Login(Arg.Any<string>(), Arg.Any<string>()).Returns(UserTestFixture.ReadUserDtoFaker.Generate());

            return userServiceFake;
            #endregion

        }
    }
}
