namespace Configuration
{
    public interface ISession
    {
        string Username { get; set; }

        bool IsAuthenticated();
    }
}
