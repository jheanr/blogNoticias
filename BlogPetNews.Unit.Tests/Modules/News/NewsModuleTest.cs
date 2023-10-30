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
using BlogPetNews.Integration.Tests.Util;
using Newtonsoft.Json;
using BlogPetNews.API.Domain.UseCases.CreateNews;

namespace BlogPetNews.Unit.Tests.Modules.News
{
    public class NewsModuleTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _application;
        private readonly HttpClient _httpClient;
        private readonly TestHelpers _testHelpers;

        public NewsModuleTest(CustomWebApplicationFactory<Program> application)
        {

            _application = application;

            _application.AddServiceFake(ServicesFakes());
            _application.AddServiceFake(AuthenticatedUser());

            _httpClient = _application.CreateClient();

            _testHelpers = new TestHelpers(_application);
        }


        [Fact]
        public async Task GetAll_News_ShouldReturnSuccess()
        {

            // Act
            var response = await _httpClient.GetAsync("/news/");

            // Assert   
            Assert.IsType<HttpResponseMessage>(response);

            _testHelpers.AssertStatusCodeOk(response);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ReadNewsDto>>(content);

            Assert.True(result!.Count == 10); 

        }

        [Fact]
        public async Task GetId_News_ShouldReturnSuccess()
        {

            //Arrange
            Guid id = Guid.NewGuid();

            // Act
            var response = await _httpClient.GetAsync($"/news/{id}");

            // Assert   
            Assert.IsType<HttpResponseMessage>(response);
            _testHelpers.AssertStatusCodeOk(response);

        }

        [Trait("Create", "Validate Create News")]
        [Fact]
        public async Task Post_News_ShouldReturnSuccess()
        {

            //Arrange
            var news = NewsTestFixture.CreateNewsDtoFaker.Generate();

            TokenTest(API.Domain.Enums.RolesUser.User);

            // Act
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/news/", news);

            // Assert
            response.EnsureSuccessStatusCode();
            _testHelpers.AssertStatusCodeOk(response);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateNewsCommandResponse>(content);

            Assert.False(result!.News.Id == Guid.Empty);

        }

        [Trait("Create", "Validate Create Invalid News")]
        [Fact]
        public async Task Post_News_ShouldReturnInvalid()
        {

            //Arrange
            var news = NewsTestFixture.CreateNewsDtoFaker.Generate();
            news.Title = "";

            TokenTest(API.Domain.Enums.RolesUser.User);

            // Act
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/news/", news);

            // Assert

            _testHelpers.AssertStatusCodeBadRequest(response);

        }

        [Trait("Delete", "Permission Delete Unauthorized")]
        [Fact]
        public async Task Delete_News_ShouldReturnForbidden()
        {

            //Arrange

            TokenTest(API.Domain.Enums.RolesUser.User);

            Guid id = Guid.NewGuid();

            // Act
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/news/{id}");

            // Assert
            _testHelpers.AssertStatusCodeForbidden(response);

        }

        [Trait("Delete", "Permission Delete")]
        [Fact]
        public async Task Delete_News_ShouldReturnSuccess()
        {

            //Arrange

            TokenTest(API.Domain.Enums.RolesUser.Admin);

            Guid id = Guid.NewGuid();

            // Act
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/news/{id}");

            // Assert
            _testHelpers.AssertStatusCodeOk(response);

        }


        [Fact]
        public async Task Update_News_ShouldReturnSuccess()
        {

            //Arrange
            var news = NewsTestFixture.UpdateNewsDtoFaker.Generate();

            TokenTest(API.Domain.Enums.RolesUser.User);

            Guid id = Guid.NewGuid();

            // Act
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/news/{id}", news);

            // Assert
            response.EnsureSuccessStatusCode();
            _testHelpers.AssertStatusCodeOk(response);


        }

        [Trait("Update", "No passing token")]
        [Fact]
        public async Task Update_News_ShouldReturnUnauthorized()
        {

            //Arrange
            var news = NewsTestFixture.UpdateNewsDtoFaker.Generate();

            Guid id = Guid.NewGuid();

            // Act
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/news/{id}", news);

            // Assert
            _testHelpers.AssertStatusCodeUnauthorized(response);


        }



        private static INewsService ServicesFakes()
        {
            #region ServiceFake
            var newsServiceFake = Substitute.For<INewsService>();

            //GetAll
            newsServiceFake.GetAll().Returns(NewsTestFixture.ReadNewsDtoFaker.Generate(10));

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

            var tokenService = _application.Services.GetRequiredService<TokenService>();

            var tokenAccess = tokenService.GenerateToken(user);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenAccess}");

        }

    }
}