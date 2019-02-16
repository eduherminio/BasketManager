using Configuration.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Configuration.Jwt
{
    public class JwtManager : IJwtManager
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly string _secret = "NotAValidSecretForSure";

        public JwtManager(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public string GenerateToken(JwtTokenPayload payload)
        {
            return _jwtTokenGenerator.GenerateToken(payload, _secret);
        }

        public string GenerateToken(JwtTokenPayload payload, int minutesTimeout)
        {
            return _jwtTokenGenerator.GenerateToken(payload, _secret, minutesTimeout);
        }

        public JwtTokenPayload GetPayload(string authHeader)
        {
            ClaimsPrincipal principal = GetPrincipal(authHeader);

            JwtTokenPayload payload = new JwtTokenPayload()
            {
                Username = GetClaim(principal, CustomClaimTypes.Name),
            };

            return payload;
        }

        public string GetTokenFromAuthorizationHeader(string authHeader)
        {
            string tokenPrefix = $"{JwtConstants.Bearer} ";
            if (authHeader.IndexOf(tokenPrefix) == 0)
            {
                return authHeader.Substring(tokenPrefix.Length);
            }
            return string.Empty;
        }

        private ClaimsPrincipal GetPrincipal(string authHeader)
        {
            try
            {
                string token = GetTokenFromAuthorizationHeader(authHeader);
                var tokenHandler = new JwtSecurityTokenHandler();

                if (!(tokenHandler.ReadToken(token) is JwtSecurityToken jwtToken))
                {
                    return null;
                }

                var symmetricKey = Encoding.UTF8.GetBytes(_secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                return tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
            }
            catch (Exception e)
            {
                throw new InvalidTokenException(e.Message, e);
            }
        }

        private static string GetClaim(ClaimsPrincipal principal, string type)
        {
            return principal.Claims.Where(c => c.Type == type).Select(c => c.Value).FirstOrDefault();
        }
    }
}
