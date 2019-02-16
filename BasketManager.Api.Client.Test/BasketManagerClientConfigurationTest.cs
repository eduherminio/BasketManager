using BasketManager.Api.Client.Configuration;
using System;
using Xunit;

namespace BasketManager.Api.Client.Test
{
    public class BasketManagerClientConfigurationTest
    {
        [Fact]
        public void CreateFromValues()
        {
            // Arrange
            Uri expectedBasketManagerUri = new Uri("http://127.0.0.1:8503/");

            // Act
            IBasketManagerClientConfiguration basketManagerConfig = BasketManagerClientConfiguration.CreateFromValues(expectedBasketManagerUri);

            // Assert
            Assert.Equal(expectedBasketManagerUri, basketManagerConfig.BasketManagerUri);
        }
    }
}
