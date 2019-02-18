using BasketManager.Api.Test.Utils;
using BasketManager.Model;
using System.Net;
using System.Net.Http;
using Xunit;

namespace BasketManager.Api.Test
{
    [Collection(BasketManagerTestCollection.Name)]
    public class BarApiTest
    {
        private readonly Fixture _fixture;

        private const string _barUri = "api/bar";

        public BarApiTest(Fixture fixture) => _fixture = fixture;

        [Fact]
        public void Add()
        {
            HttpClient client = _fixture.GetClient();

            HttpHelper.Post(client, _barUri, new Foo(), out HttpStatusCode statusCode);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void Remove()
        {
            HttpClient client = _fixture.GetClient();
            var foo = HttpHelper.Post(client, _barUri, new Foo(), out HttpStatusCode statusCode);

            object nullObject = null;
            statusCode = HttpHelper.Delete(client, $"{_barUri}/{foo.Id}", nullObject);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void ShouldNotRemoveWhenNotExisting()
        {
            HttpClient client = _fixture.GetClient("barUser");

            object nullObject = null;
            HttpStatusCode statusCode = HttpHelper.Delete(client, $"{_barUri}/5678", nullObject);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }
    }
}
