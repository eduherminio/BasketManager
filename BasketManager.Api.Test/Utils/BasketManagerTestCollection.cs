using Xunit;

namespace BasketManager.Api.Test.Utils
{
    /// <summary>
    /// Test group definition class.
    /// </summary>
    [CollectionDefinition(Name)]
    public class BasketManagerTestCollection : ICollectionFixture<Fixture>
    {
        public const string Name = "BasketManagerTestCollection";
    }
}
