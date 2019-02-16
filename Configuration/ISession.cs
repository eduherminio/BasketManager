namespace Configuration
{
    public interface ISession
    {
        string Username { get; set; }

        string Token { get; set; }

        bool IsAuthenticated();
    }
}
