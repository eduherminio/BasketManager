using BasketManager.Api.Client.Configuration;
using BasketManager.Api.Client.Wrappers;
using Configuration.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace BasketManager.Api.Client
{
    public static class BasketManagerClientServiceCollectionExtensions
    {
        private readonly static Dictionary<ServiceLifetime, Action<IServiceCollection, Type, Type>> _injectionMethods =
            new Dictionary<ServiceLifetime, Action<IServiceCollection, Type, Type>>
            {
                        { ServiceLifetime.Scoped, (services, IWrapper, WrapperImpl) => services.AddScoped(IWrapper, WrapperImpl)},
                        { ServiceLifetime.Singleton, (services, IWrapper, WrapperImpl) => services.AddSingleton(IWrapper, WrapperImpl)},
                        { ServiceLifetime.Transient, (services, IWrapper, WrapperImpl) => services.AddTransient(IWrapper, WrapperImpl)},
            };

        public static void AddBasketManagerClientWrappers(this IServiceCollection services, IConfiguration configuration)
        {
            IBasketManagerClientConfiguration clientConfiguration = BasketManagerClientConfigurationFactory.CreateFromIConfiguration(configuration);
            services.AddSingleton(new BasketManagerHttpClientWrapper());
            services.AddSingleton(clientConfiguration);
            services.AddClientWrappers(typeof(IBasketManagerClientConfiguration).Assembly);

            const int msConnectionLeaseTimeout = 60000;
            ServicePointManager.FindServicePoint(clientConfiguration.BasketManagerUri).ConnectionLeaseTimeout = msConnectionLeaseTimeout;
        }

        private static void AddClientWrappers(this IServiceCollection services, Assembly assembly)
        {
            foreach (Tuple<Type, WrapperAttribute> tuple in assembly.GetTypesAndAttributes<WrapperAttribute>())
            {
                _injectionMethods[tuple.Item2.ServiceLifetime].Invoke(services, tuple.Item2.Interface, tuple.Item1);
            }
        }
    }
}
