using Configuration;
using Configuration.Jwt;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BasketManager.Api.Client.SwaggerClient
{
    public abstract class SwaggerBaseClient
    {
        protected ISession _session;

        protected virtual Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            AddAuthorizationHeader(request);
            AddCustomHeaders(request);
            return Task.FromResult(request);
        }

        protected virtual void AddAuthorizationHeader(HttpRequestMessage request)
        {
            string token = GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtConstants.Bearer, token);
            }
        }

        protected virtual void AddCustomHeaders(HttpRequestMessage request) { }

        protected string GetToken()
        {
            return _session?.Token;
        }
    }
}

