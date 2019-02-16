using Configuration.Exceptions;
using Configuration.Jwt;
using Configuration.Logs;

namespace BasketManager.Service.Impl
{
    [Log]
    [ExceptionManagement]
    public class LoginService : ILoginService
    {
        private readonly IJwtManager _jwtManager;

        public LoginService(IJwtManager jwtManager)
        {
            _jwtManager = jwtManager;
        }

        public string GenerateToken(string username)
        {
            return GenerateTokenForUser(username);
        }

        public string RenewToken(string authHeader)
        {
            JwtTokenPayload payload = _jwtManager.GetPayload(authHeader);

            return GenerateTokenForUser(payload.Username);
        }

        private string GenerateTokenForUser(string username)
        {
            JwtTokenPayload payload = new JwtTokenPayload()
            {
                Username = username
            };

            return _jwtManager.GenerateToken(payload);
        }
    }
}
