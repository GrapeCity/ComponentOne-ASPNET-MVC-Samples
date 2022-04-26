using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

#if NETCORE31
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
#endif

namespace DashboardDemo
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

#if NETCORE31
            // Allowing Sync
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });            
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
#endif
        }

#if NETCORE31
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#else
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
#endif
        {
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
            app.UseReportProviders().AddFlexReportDiskStorage("Content", Path.Combine(env.WebRootPath, "Content"));
            
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
