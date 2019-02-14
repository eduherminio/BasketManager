using System;
using System.Reflection;
using AspectCore.Extensions.DependencyInjection;
using Configuration.Exceptions;
using Configuration.Helpers;
using Configuration.Jwt;
using Configuration.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BasketManager.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private static readonly ApiVersion _apiVersion = new ApiVersion(1, 0);

        protected ApiInfo ApiInfo => new ApiInfo(
            "BasketManager API",
            "API for managing customer's baskets",
            _apiVersion);

        protected string SwaggerDocumentVersion { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            SwaggerDocumentVersion = $"v{ApiInfo.ApiVersion}";
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddOptions();

            services.AddJwtServices();

            // Api versioning
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiInfo.ApiVersion;
            });

            // Swagger
            string assemblyName = GetType().GetTypeInfo().Assembly.GetName().Name;
            services.ConfigureSwaggerMvcServices(SwaggerDocumentVersion, ApiInfo, assemblyName);

            services.AddAuthenticationCore();
            services.AddAuthorizationPolicyEvaluator();

            //services.AddMvc(
            //    options => options.Filters.Add(typeof(ExceptionFilterAttribute)))
            //    .AddJsonOptions(options => options.SerializerSettings.AddCustomJsonSerializerSettings());

            //services.AddMvcCore(options => options.Filters.Add(typeof(ExceptionFilterAttribute)))
            //    .AddJsonOptions(options => options.SerializerSettings.AddCustomJsonSerializerSettings())
            //    .AddApiExplorer();
            services.AddMvcCore().AddApiExplorer();
            services.AddMvc(
                options => options.Filters.Add(typeof(ExceptionFilterAttribute)))
                .AddJsonOptions(options => options.SerializerSettings.AddCustomJsonSerializerSettings());

            services.AddBasketManagerServices();

            return services.BuildAspectInjectorProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.ConfigureSwaggerMvc(SwaggerDocumentVersion);

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        //private void WireDbContext<TDbContext>(IServiceCollection services) where TDbContext : DbContext
        //{
        //    string connectionString = Configuration["ConnectionString"];
        //    string providerString = Configuration["Provider"];

        //    services.AddDbContext<TDbContext>(GetDatabaseOptions(providerString, connectionString), ServiceLifetime.Scoped);
        //}

        //private Action<DbContextOptionsBuilder> GetDatabaseOptions(string providerString, string connectionString)
        //{
        //    return (builder) =>
        //    {
        //        if (!string.IsNullOrEmpty(providerString))
        //        {
        //            builder.UseSqlServer(connectionString);
        //            builder.EnableSensitiveDataLogging();
        //        }
        //    };
        //}
    }
}
