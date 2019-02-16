using Configuration;
using BasketManager.Api.Client.Wrappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace BasketManager.Api.Client.Test
{
    public class ServiceCollectionExtensionsTest
    {
        [Fact]
        public void ServiceProviderConfiguration()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    { "BasketManagerUri", "http://localhost" }
                })
                .Build();

            IServiceCollection services = new ServiceCollection();
            services.AddScoped<ISession, Session>();
            services.AddBasketManagerClientWrappers(configuration);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                Assert.NotNull(scope.ServiceProvider.GetRequiredService<IFooClient>());
                Assert.NotNull(scope.ServiceProvider.GetRequiredService<IBarClient>());
                Assert.NotNull(scope.ServiceProvider.GetRequiredService<IITemClient>());
            }
        }
    }
}
