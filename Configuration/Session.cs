namespace Configuration
{
    public class Session : ISession
    {
        public string Username { get; set; }

        public string Token { get; set; }

        public Session()
        {
            Username = string.Empty;
            Token = string.Empty;
        }

        public bool IsAuthenticated()
        {
            return Username != string.Empty;
        }
    }
}
