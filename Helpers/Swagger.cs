using System;
using System.Collections.Generic;
using System.IO;
using DotNetCoreSwap.Helpers;
using DotNetCoreSwap.Models;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotNetCoreSwap
{
    internal static class Swagger
    {
        public static void ConfigureSwagger(SwaggerGenOptions options)
        {
            string securityName = "Basic";

            options.SwaggerDoc("v1.0", new Info { Title = "DotNetCoreSwap API", Version = "v1.0" });
            options.AddSecurityDefinition(securityName, new ApiKeyScheme()
                {
                    Description = "Enter: Basic {token}",
                    Name = "Authorization",
                    In = "header"
                });
            options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    [securityName] = new string[0]
                });
            options.OperationFilter<AddResponseHeadersFilter>();

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, typeof(Startup).Namespace + ".xml"));

            var sampleValues = new Dictionary<string, object>
            {
                ["UserRequest.username"] = "test",
                ["UserRequest.password"] = "test",
                ["UserResponse.firstName"] = "T",
                ["UserResponse.lastName"] = "Est",
                ["UserResponse.username"] = "Test",
                ["IntSortResults.unsortedArray"] = new int[] {5,89,123,2,65,23,823,52},
                ["IntSortResults.milliseconds"] = "2",
                ["IntSortResults.sortedArray"] = new int[] {2,5,23,52,65,89,123,823}
            };

            options.DocumentFilter<SwaggerExampleFilter>(sampleValues);
            options.DocumentFilter<DocumentationFilter>();
        }
    }
}
