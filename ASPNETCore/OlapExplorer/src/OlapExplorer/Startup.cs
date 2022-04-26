using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OlapExplorer.Models;

#if NETCORE31
using Microsoft.Extensions.Hosting;
#endif

namespace OlapExplorer
{
    public class Startup
    {
#if NETCORE31
        public Startup(IWebHostEnvironment env)
#else
        public Startup(IHostingEnvironment env)
#endif
        {
            Environment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

#if NETCORE31
        public static IWebHostEnvironment Environment { get; set; }
#else
        public static IHostingEnvironment Environment { get; set; }
#endif

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
#if NETCORE31
                .AddNewtonsoftJson()
#endif
            ;
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#if NETCORE31
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#else
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
#endif
        {
            var defaultCulture = "en-US";
            var supportedCultures = new[]
            {
                new CultureInfo(defaultCulture)
            };

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

#if NETCORE31
            app.UseRouting();
#endif

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

#if NETCORE31
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Olap}/{action=Index}/{id?}");
            });
#else
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Olap}/{action=Index}/{id?}");
            });
#endif

            app.UseDataEngineProviders()
                .AddDataEngine("complex10", () => ProductData.GetData(100000))
                .AddDataSource("dataset10", () => ProductData.GetData(100000).ToList())
                .AddCube("cube", @"Data Source=http://ssrs.componentone.com/OLAP/msmdpump.dll;Provider=msolap;Initial Catalog=AdventureWorksDW2012Multidimensional", "Adventure Works");
        }
    }
}
