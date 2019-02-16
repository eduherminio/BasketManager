using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace BasketManager.Api.Test.Utils
{
    public class Fixture : IDisposable
    {
        public static readonly string DefaultString = "testUser";

        public readonly TestServer Server;

        public Fixture()
        {
            Server = TestUtils.CreateTestServer<Startup>();
        }

        public HttpClient GetNonAuthClient() => TestUtils.GetHttpClient(Server);

        public HttpClient GetClient() => TestUtils.GetAuthHttpClient(Server, Server, DefaultString);

        public HttpClient GetClient(string username) => TestUtils.GetAuthHttpClient(Server, Server, username);

        public TService GetService<TService>()
        {
            return (TService)Server.Host.Services.GetService(typeof(TService));
        }

        protected virtual void Dispose(bool disposing)
        {
            // Delete DB, if any
            Server.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
