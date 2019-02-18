using BasketManager.Api.Test.Utils;
using BasketManager.Model;
using System.Net;
using System.Net.Http;
using Xunit;

namespace BasketManager.Api.Test
{
    [Collection(BasketManagerTestCollection.Name)]
    public class FooApiTest
    {
        private readonly Fixture _fixture;

        private const string _fooUri = "api/foo";

        public FooApiTest(Fixture fixture) => _fixture = fixture;

        [Fact]
        public void Add()
        {
            HttpClient client = _fixture.GetClient();

            HttpHelper.Post(client, _fooUri, new Foo(), out HttpStatusCode statusCode);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void Remove()
        {
            HttpClient client = _fixture.GetClient();
            var foo = HttpHelper.Post(client, _fooUri, new Foo(), out HttpStatusCode statusCode);

            object nullObject = null;
            statusCode = HttpHelper.Delete(client, $"{_fooUri}/{foo.Id}", nullObject);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void ShouldNotRemoveWhenNotExisting()
        {
            HttpClient client = _fixture.GetClient("fooUser");

            object nullObject = null;
            HttpStatusCode statusCode = HttpHelper.Delete(client, $"{_fooUri}/1234", nullObject);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }
    }
}
