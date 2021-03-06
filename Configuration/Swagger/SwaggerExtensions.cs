﻿using Configuration.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

namespace Configuration.Swagger
{
    public static class SwaggerExtensions
    {
        public static void ConfigureSwaggerMvcServices(
            this IServiceCollection services, string SwaggerDocumentVersion, ApiInfo apiInfo, string assemblyName)
        {
            string packageVersion = PlatformServices.Default.Application.ApplicationVersion;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SwaggerDocumentVersion,
                    new Info
                    {
                        Version = packageVersion,
                        Title = apiInfo.Title,
                        Description = apiInfo.Description,
                        TermsOfService = "Eduardo Cáceres de la Calle, all rights reserved",
                        Contact = new Contact() { Name = "Eduardo Cáceres de la Calle" },
                        License = new License { Name = "GNU GENERAL PUBLIC LICENSE Version 3", Url = "https://github.com/eduherminio/BasketManager/blob/master/LICENSE" }
                    }
                );

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                // Allow same class names in different namespaces
                c.CustomSchemaIds(x => x.FullName);

                // Set the comments path for the Swagger JSON and UI.
                c.IncludeXmlComments(StartupHelpers.GetXmlCommentsFilePath(assemblyName));

                // Avoid duplicating enums in Open.Api spec.
                c.UseReferencedDefinitionsForEnums();
            });
        }

        public static void ConfigureSwaggerMvc(this IApplicationBuilder app, string SwaggerDocumentVersion)
        {
            app.UseSwagger(c => c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value));

            app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{SwaggerDocumentVersion}/swagger.json", $"{SwaggerDocumentVersion} docs"));
        }
    }
}
