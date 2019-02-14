using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Configuration.Jwt
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private const int _defaultMinutesTimeout = 15;

        public string GenerateToken(JwtTokenPayload payload, string secret)
        {
            return GenerateToken(payload, secret, _defaultMinutesTimeout);
        }

        public string GenerateToken(JwtTokenPayload payload, string secret, int minutesTimeout)
        {
            byte[] symmetricKey = Encoding.UTF8.GetBytes(secret);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            List<Claim> listClaims = new List<Claim>
            {
                new Claim(CustomClaimTypes.Name, payload.Username),
            };

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(listClaims),
                Expires = DateTime.UtcNow.AddMinutes(minutesTimeout),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken stoken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(stoken);
        }
    }
}
