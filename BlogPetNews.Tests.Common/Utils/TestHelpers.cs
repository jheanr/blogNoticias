using BlogPetNews.API.Domain.Enums;
using BlogPetNews.API.Domain.UseCases.CreateNews;
using BlogPetNews.API.Domain.UseCases.LoginUser;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Infra.Contexts;
using BlogPetNews.API.Infra.Utils;
using BlogPetNews.API.Service.ViewModels.News;
using BlogPetNews.Tests.Common.Factory;
using BlogPetNews.Tests.Common.News;
using BlogPetNews.Tests.Common.Users;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace BlogPetNews.Tests.Common.Util
{
    public class TestHelpers
    {

        private readonly CustomWebApplicationFactory<Program> _application;
        private readonly HttpClient _httpClient;
        private readonly ICryptography _cryptography;

        public TestHelpers(CustomWebApplicationFactory<Program> application)
        {
            _application = application;
            _httpClient = application.CreateClient();
            _cryptography = new Cryptography();
        }

        #region Asserts
        public void AssertStatusCodeOk(HttpResponseMessage response)
        {
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public void AssertStatusCodeBadRequest(HttpResponseMessage response)
        {
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        public void AssertStatusCodeNotFound(HttpResponseMessage response)
        {
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        public void AssertStatusCodeUnauthorized(HttpResponseMessage response)
        {
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        public void AssertStatusCodeForbidden(HttpResponseMessage response)
        {
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        public void AssertStatusCodeInternalServerErrorRequest(HttpResponseMessage response)
        {
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
        #endregion


        #region Mocks
        public async Task<User> Createuser(CustomWebApplicationFactory<Program> _application, User user)
        {
            using (var scope = _application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var petNewsDbContext = provider.GetRequiredService<BlogPetNewsDbContext>())
                {
                    await petNewsDbContext.Database.EnsureCreatedAsync();
                    await petNewsDbContext.Users.AddAsync(user);

                    await petNewsDbContext.SaveChangesAsync();
                    return user;
                }
            }
        }

        public async Task<API.Domain.News.News> CreateNews(CustomWebApplicationFactory<Program> _application, API.Domain.News.News news)
        {
            using (var scope = _application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var petNewsDbContext = provider.GetRequiredService<BlogPetNewsDbContext>())
                {
                    await petNewsDbContext.Database.EnsureCreatedAsync();
                    await petNewsDbContext.News.AddAsync(news);

                    await petNewsDbContext.SaveChangesAsync();

                    return news;
                }
            }
        }
        #endregion


        #region Helpers
        public async Task<HttpClient> LoginUser(bool userIsAdmin)
        {
            //Arrange
            var user = UserTestFixture.UserFaker.Generate();
            var tempPassword = user.Password;
            user.Password = _cryptography.Encodes(user.Password);

            if (userIsAdmin)
                user.Role = RolesUser.Admin;
            else
                user.Role = RolesUser.User;

            await Createuser(_application, user);
            var httpResponse = await _httpClient.PostAsync($"/login?email={user.Email}&password={tempPassword}", null);

            var content = await httpResponse.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<LoginUserCommandResponse>(content);

            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + result!.Token);

            return _httpClient;
        }

        public async Task<ReadNewsDto> CreateNews(HttpClient client)
        {
            var news = NewsTestFixture.CreateNewsDtoFaker.Generate();
            var response = await _httpClient.PostAsJsonAsync("/news", news);

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CreateNewsCommandResponse>(content);

            AssertStatusCodeOk(response);

            return result!.News;

        }

        #endregion

    }
}
