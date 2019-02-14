namespace Configuration.Jwt
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(JwtTokenPayload payload, string secret);

        string GenerateToken(JwtTokenPayload payload, string secret, int minutesTimeout);
    }
}
