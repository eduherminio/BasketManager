using BasketManager.Api.Client.Configuration;
using Configuration.Helpers;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace BasketManager.Api.Client.Test
{
    public class BasketManagerClientConfigurationFactoryTest
    {
        [Fact]
        public void CreateFromIConfiguration()
        {
            // Arrange
            const string expectedBasketManagerUri = "http://127.0.0.1:8503/";

            IConfiguration configuration = AppSettingsHelpers.GetConfiguration();

            configuration["BasketManagerUri"] = expectedBasketManagerUri;

            // Act
            IBasketManagerClientConfiguration basketManagerConfig = BasketManagerClientConfigurationFactory.CreateFromIConfiguration(configuration);

            // Assert
            Assert.Equal(expectedBasketManagerUri, basketManagerConfig.BasketManagerUri.AbsoluteUri);
        }
    }
}
