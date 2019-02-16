using BasketManager.Api.Test.Utils;
using BasketManager.Model;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace BasketManager.Api.Test
{
    [Collection(BasketManagerTestCollection.Name)]
    public class ItemApiTest
    {
        private readonly Fixture _fixture;

        private const string _itemUri = "api/items";

        public ItemApiTest(Fixture fixture) => _fixture = fixture;

        [Fact]
        public void Load()
        {
            HttpClient client = _fixture.GetClient("NewUser");

            ICollection<Item> items = HttpHelper.Get<ICollection<Item>>(client, _itemUri, out HttpStatusCode statusCode);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Empty(items);
        }

        [Fact]
        public void Clear()
        {
            HttpClient client = _fixture.GetClient();

            object nullObject = null;
            HttpStatusCode statusCode = HttpHelper.Delete(client, _itemUri, nullObject);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
    }
}
