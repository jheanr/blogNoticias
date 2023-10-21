using BlogPetNews.API.Domain.UseCases.LoginUser;
using BlogPetNews.API.Service.Users;
using BlogPetNews.API.Service.ViewModels.Users;
using BlogPetNews.Tests.Common.Factory;
using BlogPetNews.Tests.Common.Users;
using Newtonsoft.Json;
using NSubstitute;
using System.Net;
using System.Net.Http.Json;

namespace BlogPetNews.Unit.Tests.Modules.Users
{
    public class UserModuleTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public UserModuleTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;

            _factory.AddServiceFake(ServicesFakes());

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Login_User_ShouldReturnSuccess()
        {

            //Arrange

            var login = new { Email = "test@test.com", Password = "123456" };

            //Act

            HttpResponseMessage response = await _client.PostAsync($"/login?email={login.Email}&password={login.Password}", null);

            //Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginUserCommandResponse>(content);

            Assert.DoesNotContain(result!.Token, "Unauthorized Access");
        

        }


        [Trait("Type", "Validate Create User")]
        [Fact]
        public async Task Create_User_ShouldReturnSuccess()
        {
            //Arrange

            var user = UserTestFixture.CreateUserDtoFaker.Generate();

            //Act
            HttpResponseMessage response = await _client.PostAsJsonAsync("/create/", user);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Trait("Type", "Validate Create User")]
        [Fact]
        public async Task Create_User_ShouldReturnInvalid()
        {
            //Arrange

            var user = UserTestFixture.CreateUserDtoFaker.Generate();
            user.Email = "test";
            user.Password = "";

            //Act
            HttpResponseMessage response = await _client.PostAsJsonAsync("/create/", user);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }



        private static IUserService ServicesFakes()
        {
            var userServiceFake = Substitute.For<IUserService>();

            //Create
            userServiceFake.Create(Arg.Any<CreateUserDto>()).Returns(UserTestFixture.ReadUserDtoFaker.Generate());

            //Login
            userServiceFake.Login(Arg.Any<string>(), Arg.Any<string>()).Returns(UserTestFixture.ReadUserDtoFaker.Generate());

            return userServiceFake;
        }
    }
}
