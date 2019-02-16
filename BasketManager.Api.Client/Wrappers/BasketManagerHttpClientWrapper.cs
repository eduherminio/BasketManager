using System.Net.Http;

namespace BasketManager.Api.Client.Wrappers
{
    public class BasketManagerHttpClientWrapper
    {
        public HttpClient HttpClient { get; }

        public BasketManagerHttpClientWrapper()
        {
            HttpClient = new HttpClient();
        }
    }
}
