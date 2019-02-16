using Configuration;
using System;
using System.Net.Http;

namespace BasketManager.Api.Client.SwaggerClient
{
    public partial class BasketManagerClient : SwaggerBaseClient
    {
        public BasketManagerClient(Uri uri, HttpClient httpClient, ISession session) : this(uri.AbsoluteUri, httpClient)
        {
            _session = session;
        }
    }
}

