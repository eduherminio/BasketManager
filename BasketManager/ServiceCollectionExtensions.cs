using BasketManager.Dao;
using BasketManager.Dao.Impl;
using BasketManager.Service;
using BasketManager.Service.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace BasketManager
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBasketManagerServices(this IServiceCollection services)
        {
            services.AddScoped<IBasketManagerService, BasketManagerService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IItemDao, ItemDao>();
            services.AddSingleton(new DatabaseFixture());
        }
    }
}
