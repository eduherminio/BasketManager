﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Configuration.Logs;
using Configuration.Helpers;
using Configuration.Jwt;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BasketManager.Api.Test.Utils
{
    public static class TestUtils
    {
        public static TestServer CreateTestServer<TStartup>()
            where TStartup : class
        {
            return CreateTestServer<TStartup>(AppSettingsHelpers.GetConfiguration());
        }

        public static TestServer CreateTestServer<TStartup>(IConfiguration configuration)
            where TStartup : class
        {
            return CreateTestServer<TStartup>(configuration, (_) => { });
        }

        public static TestServer CreateTestServer<TStartup>(IConfiguration configuration, Action<IServiceCollection> configureServices)
            where TStartup : class
        {
            var hostBuilder = new WebHostBuilder()
                .UseConfiguration(configuration)
                .ConfigureServices(configureServices)
                .UseStartup<TStartup>()
                .UseCommonLogs();

            return new TestServer(hostBuilder);
        }

        internal static string GetAuthToken(TestServer authServer, string userName)
        {
            var httpClient = GetHttpClient(authServer);
            string tokenUri = $"/api/login?username={userName}";
            string token = HttpHelper.Post(httpClient, tokenUri, string.Empty, out HttpStatusCode statusCode);

            if (statusCode != HttpStatusCode.OK)
            {
                throw new Exception("Login failed");
            }

            return token;
        }

        public static HttpClient GetHttpClient(TestServer server) => server.CreateClient();

        public static HttpClient GetAuthHttpClient(TestServer server, TestServer authServer, string username)
        {
            HttpClient client = server.CreateClient();
            if (authServer != null)
            {
                string authToken = GetAuthToken(authServer, username);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtConstants.Bearer, authToken);
            }
            return client;
        }
    }
}
