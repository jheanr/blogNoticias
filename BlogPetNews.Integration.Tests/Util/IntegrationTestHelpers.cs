using BlogPetNews.API.Service.News;
using BlogPetNews.API.Service.Users;
using BlogPetNews.API.Service.ViewModels.News;
using BlogPetNews.API.Service.ViewModels.Users;
using BlogPetNews.Tests.Common.News;
using BlogPetNews.Tests.Common.Users;
using NSubstitute;
using System.Net;

namespace BlogPetNews.Integration.Tests.Util
{
    public class IntegrationTestHelpers
    {
        #region Asserts
        public static void AssertStatusCodeOk(HttpResponseMessage response)
        {
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public static void AssertStatusCodeBadRequest(HttpResponseMessage response)
        {
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        public static void AssertStatusCodeUnauthorized(HttpResponseMessage response)
        {
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        public static void AssertStatusCodeInternalServerErrorRequest(HttpResponseMessage response)
        {
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
        #endregion


        #region ServiceFakes
        internal static INewsService NewsServiceFake()
        {

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

        }
        internal static IUserService UserServiceFake()
        {

            var userServiceFake = Substitute.For<IUserService>();

            //Create
            userServiceFake.Create(Arg.Any<CreateUserDto>()).Returns(UserTestFixture.ReadUserDtoFaker.Generate());

            //Login
            userServiceFake.Login(Arg.Any<string>(), Arg.Any<string>()).Returns(UserTestFixture.ReadUserDtoFaker.Generate());

            return userServiceFake;

        }
        #endregion


    }
}
