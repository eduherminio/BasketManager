namespace Configuration.Jwt
{
    public class JwtTokenPayload
    {
        public string Username { get; set; }

        public JwtTokenPayload()
        {
            Username = string.Empty;
        }
    }
}
