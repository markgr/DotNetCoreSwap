using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreSwap.Extensions;
using DotNetCoreSwap.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;


namespace DotNetCoreSwap
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			string securityName = "Basic";

			services.AddMvc(options => 
			{
				// Only support JSON by default.
				options.InputFormatters.RemoveType<JsonPatchInputFormatter>();
				options.OutputFormatters.RemoveType<StringOutputFormatter>();

				var policy = new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.Build();

				options.Filters.Add(new AuthorizeFilter(policy));

			})
			.AddJsonOptions(options =>
			{
				options.SerializerSettings.Converters.Add(new StringEnumConverter());
				options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
			})
			.AddApplicationPart(typeof(Startup).Assembly)
			.AddControllersAsServices()
			.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddSwaggerGen( c=> {
				c.SwaggerDoc("v1.0", new Info { Title = "DotNetCoreSwap API", Version = "v1.0" });
				c.AddSecurityDefinition(securityName, new ApiKeyScheme()
				{
					Description = "Enter: Basic {token}",
					Name = "Authorization",
					In = "header"
				});
				c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
				{
					[securityName] = new string[0]
				});
			});

            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

			app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "DotNetCoreSwap v1.0");
            });

            app.UseMvc();
        }
    }
}
