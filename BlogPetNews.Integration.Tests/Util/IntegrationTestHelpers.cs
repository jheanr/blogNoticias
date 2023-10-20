using System.Net;

namespace BlogPetNews.Integration.Tests.Util
{
    public class IntegrationTestHelpers
    {
        public static void AssertStatusCodeOk(HttpResponseMessage response)
        {
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public static void AssertStatusCodeBadRequest(HttpResponseMessage response)
        {
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        public static void AssertStatusCodeUnauthorizedd(HttpResponseMessage response)
        {
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        public static void AssertStatusCodeInternalServerErrorRequest(HttpResponseMessage response)
        {
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
