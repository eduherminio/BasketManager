using Microsoft.Extensions.DependencyInjection;

namespace Configuration.Jwt
{
    public static class JwtServiceCollectionExtensions
    {
        public static void AddJwtServices(this IServiceCollection services)
        {
            services.AddScoped<ISession, Session>();
            services.AddSingleton<IJwtManager, JwtManager>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        }
    }
}
