namespace BasketManager.Service
{
    public interface ILoginService
    {
        string GenerateToken(string username);

        string RenewToken(string authHeader);
    }
}
