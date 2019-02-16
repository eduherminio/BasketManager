using BasketManager.Service;
using BasketManager.Service.Impl;
using Configuration.Jwt;
using Xunit;

namespace BasketManager.Test
{
    public class LoginServiceTest
    {
        private readonly IJwtManager _jwtManager;
        private readonly ILoginService _loginService;

        private readonly string _username = "test";

        public LoginServiceTest()
        {
            _jwtManager = new JwtManager(new JwtTokenGenerator());
            _loginService = new LoginService(_jwtManager);
        }

        [Fact]
        public void GenerateToken()
        {
            string token = _loginService.GenerateToken(_username);
            Assert.NotSame(string.Empty, token);

            JwtTokenPayload payload = _jwtManager.GetPayload($"Bearer {token}");
            Assert.Equal(_username, payload.Username);
        }

        [Fact]
        public void RenewToken()
        {
            string token = _loginService.GenerateToken(_username);
            Assert.NotSame(string.Empty, token);

            string renewedToken = _loginService.RenewToken($"Bearer {token}");
            Assert.NotSame(string.Empty, renewedToken);
        }
    }
}