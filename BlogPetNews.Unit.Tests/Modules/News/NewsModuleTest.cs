using BlogPetNews.API.Service.News;
using BlogPetNews.API.Service.Users;
using BlogPetNews.API.Service.ViewModels.News;
using BlogPetNews.Tests.Common.Factory;
using BlogPetNews.Tests.Common.News;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Net.Http.Json;
using BlogPetNews.API.Infra.Utils;
using BlogPetNews.Tests.Common.Users;
using System.Net;

namespace BlogPetNews.Unit.Tests.Modules.News
{
    public class NewsModuleTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public NewsModuleTest(CustomWebApplicationFactory<Program> factory)
        {

            _factory = factory;

            _factory.AddServiceFake(ServicesFakes());
            _factory.AddServiceFake(AuthenticatedUser());

            _client = _factory.CreateClient();

        }


        [Fact]
        public async Task GetAll_News_ShouldReturnSuccess()
        {

            // Act
            var response = await _client.GetAsync("/news/");

            // Assert   
            Assert.IsType<HttpResponseMessage>(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task GetId_News_ShouldReturnSuccess()
        {

            //Arrange
            Guid id = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/news/{id}");

            // Assert   
            Assert.IsType<HttpResponseMessage>(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Trait("Create", "Validate Create News")]
        [Fact]
        public async Task Post_News_ShouldReturnSuccess()
        {

            //Arrange
            var news = NewsTestFixture.CreateNewsDtoFaker.Generate();

            TokenTest(API.Domain.Enums.RolesUser.User);

            // Act
            HttpResponseMessage response = await _client.PostAsJsonAsync("/news/", news);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Trait("Create", "Validate Create News Invalid")]
        [Fact]
        public async Task Post_News_ShouldReturnInvalid()
        {

            //Arrange
            var news = NewsTestFixture.CreateNewsDtoFaker.Generate();
            news.Title = "";

            TokenTest(API.Domain.Enums.RolesUser.User);

            // Act
            HttpResponseMessage response = await _client.PostAsJsonAsync("/news/", news);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Trait("Delete", "Permission Delete Unauthorized")]
        [Fact]
        public async Task Delete_News_ShouldReturnUnauthorized()
        {

            //Arrange

            TokenTest(API.Domain.Enums.RolesUser.User);

            Guid id = Guid.NewGuid();

            // Act
            HttpResponseMessage response = await _client.DeleteAsync($"/news/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);

        }

        [Trait("Delete", "Permission Delete")]
        [Fact]
        public async Task Delete_News_ShouldReturnSuccess()
        {

            //Arrange

            TokenTest(API.Domain.Enums.RolesUser.Admin);

            Guid id = Guid.NewGuid();

            // Act
            HttpResponseMessage response = await _client.DeleteAsync($"/news/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }


        [Fact]
        public async Task Update_News_ShouldReturnSuccess()
        {

            //Arrange
            var news = NewsTestFixture.UpdateNewsDtoFaker.Generate();

            TokenTest(API.Domain.Enums.RolesUser.User);

            Guid id = Guid.NewGuid();

            // Act
            HttpResponseMessage response = await _client.PutAsJsonAsync($"/news/{id}", news);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }


        private static INewsService ServicesFakes()
        {
            #region ServiceFake
            var newsServiceFake = Substitute.For<INewsService>();

            //GetAll
            newsServiceFake.GetAll().Returns(NewsTestFixture.ReadNewsDtoFaker.Generate(3));

            //GetId
            newsServiceFake.GetById(Arg.Any<Guid>()).Returns(NewsTestFixture.ReadNewsDtoFaker.Generate());

            //Update
            newsServiceFake.Update(Arg.Any<Guid>(), Arg.Any<UpdateNewsDto>()).Returns(NewsTestFixture.ReadNewsDtoFaker.Generate());

            //Delete
            newsServiceFake.Delete(Arg.Any<Guid>());

            //Post 
            newsServiceFake.Create(Arg.Any<CreateNewsDto>(), Arg.Any<Guid>()).Returns(NewsTestFixture.ReadNewsDtoFaker.Generate());

            return newsServiceFake;
            #endregion

        }

        private static IUserService AuthenticatedUser()
        {

            //User
            var userServiceFake = Substitute.For<IUserService>();
            userServiceFake.GetByEmail(Arg.Any<string>()).Returns(UserTestFixture.ReadUserDtoFaker.Generate());

            return userServiceFake;

        }

        private void TokenTest(API.Domain.Enums.RolesUser role)
        {
            var user = UserTestFixture.UserFaker.Generate();
            user.Role = role;

            var tokenService = _factory.Services.GetRequiredService<TokenService>();

            var tokenAccess = tokenService.GenerateToken(user);
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenAccess}");

        }

    }
}