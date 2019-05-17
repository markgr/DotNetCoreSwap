using DotNetCoreSwap.Extensions;
using DotNetCoreSwap.Interfaces;
using DotNetCoreSwap.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;


namespace DotNetCoreSwap
{
    /// <summary>
    /// Self hosted webapi
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:DotNetCoreSwap.Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add MVC options
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

            // Add the swagger configuration
			services.AddSwaggerGen(Swagger.ConfigureSwagger);

            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<ISort, InsertionSort>();
            services.AddSingleton<ISort, SelectionSort>();
            services.AddSingleton<ISort, BubbleSort>();
            services.AddSingleton<ISort, QuickSort>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">App.</param>
        /// <param name="env">Env.</param>
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
