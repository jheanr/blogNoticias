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

        public static void AssertStatusCodeNotFound(HttpResponseMessage response)
        {
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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

    }
}
