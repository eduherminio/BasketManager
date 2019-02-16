using BasketManager.Api.Test.Utils;
using Configuration.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace BasketManager.Api.Test
{
    [Collection(BasketManagerTestCollection.Name)]
    public class LoginApiTest
    {
        private readonly Fixture _fixture;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = new JwtTokenGenerator();

        private const string _loginUri = "api/login";
        private readonly string _renewUri = $"{_loginUri}/renew";

        public LoginApiTest(Fixture fixture) => _fixture = fixture;

        [Fact]
        public void Login()
        {
            HttpClient client = _fixture.GetClient();

            JwtManager jwtManager = new JwtManager(_jwtTokenGenerator);
            JwtTokenPayload payload = jwtManager.GetPayload(client.DefaultRequestHeaders.Authorization.ToString());

            ValidateToken(payload);
        }

        [Fact]
        public void RenewToken()
        {
            HttpClient client = _fixture.GetClient();

            string newToken = HttpHelper.Post<string>(client, _renewUri, null, out HttpStatusCode statusCode);
            AuthenticationHeaderValue authHeader = new AuthenticationHeaderValue("Bearer", newToken);

            JwtManager jwtManager = new JwtManager(_jwtTokenGenerator);
            JwtTokenPayload payload = jwtManager.GetPayload(authHeader.ToString());

            ValidateToken(payload);
        }

        private void ValidateToken(JwtTokenPayload payload)
        {
            Assert.Equal(Fixture.DefaultString, payload.Username);
        }
    }
}
