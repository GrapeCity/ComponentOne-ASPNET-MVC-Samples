using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FlightStatistics.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#if NETCORE31
using Microsoft.Extensions.Hosting;
#endif

namespace FlightStatistics
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
        public IConfiguration Configuration { get; }
#if NETCORE31
public static IWebHostEnvironment Environment { get; set; }
#else
        public static IHostingEnvironment Environment { get; set; }
#endif

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AirportEntities>(o => o.UseSqlite(GetConnectionString()));
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
            });

            C1.Web.Mvc.LicenseManager.Key = License.Key;
            services.AddMvc(
#if NETCORE31
                options => options.EnableEndpointRouting = false
#endif
                );
            services.AddMvc()

#if NETCORE31
               .AddNewtonsoftJson()
#endif
            ;
        }

        private string GetConnectionString()
        {
            var configConnectionString = Configuration["ConnectionStrings:ConnectionString"];
            const char folderSeparator = '\\';
            var dataFolderPath = Environment.WebRootPath.Replace('/', folderSeparator);
            if (dataFolderPath.Last() != folderSeparator)
            {
                dataFolderPath += folderSeparator;
            }

            var dataSourceText = "data source=";
            var index = configConnectionString.IndexOf(dataSourceText);
            if (index != -1)
            {
                return configConnectionString.Insert(index + dataSourceText.Length, dataFolderPath);
            }

            return configConnectionString;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#if NETCORE31
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#else
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
#endif
        {
            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            // do not change the name of defaultCulture
            var defaultCulture = "en-US";
            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo(defaultCulture)
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
#if NETCORE31
            app.UseRouting();
#endif

#if NETCORE31
            app.UseEndpoints(endpoints =>
            {
               endpoints.MapControllerRoute(
                   name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
#else
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
#endif
        }
    }
}
