using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.IO;
#if NETCORE31 || NET50
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
#endif
using System.Data.Common;

namespace FlexViewerExplorer
{
    public class Startup
    {

#if NETCORE31 || NET50
        public static IWebHostEnvironment Environment { get; set; }
#else
        public static IHostingEnvironment Environment { get; set; }
#endif

        public IConfigurationRoot Configuration { get; set; }

#if NETCORE31 || NET50
        public Startup(IWebHostEnvironment env)
#else
        public Startup(IHostingEnvironment env)
#endif
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            Environment = env;

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSession();

#if NETCORE31 || NET50
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#if NETCORE31 || NET50
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#else
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseStaticFiles();
            app.UseSession();

#if NETCORE31 || NET50
            app.UseRouting();
#endif

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

#if NETCORE31 || NET50
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "FlexViewer", action = "Intro" });

            });
#else
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "FlexViewer", action = "Intro" });
            });

#endif

            app.UseStorageProviders().AddDiskStorage("PdfsRoot", Path.Combine(env.WebRootPath, "PdfsRoot"));
            app.UseReportProviders().AddFlexReportDiskStorage("ReportsRoot", Path.Combine(env.WebRootPath, "ReportsRoot"));

            var ssrsUrl = Configuration["AppSettings:SsrsUrl"];
            var ssrsUserName = Configuration["AppSettings:SsrsUserName"];
            var ssrsPassword = Configuration["AppSettings:SsrsPassword"];
            app.UseReportProviders().AddSsrsReportHost("c1ssrs", ssrsUrl, new System.Net.NetworkCredential(ssrsUserName, ssrsPassword));
            
#if NETCORE31 || NET50
            DbProviderFactories.RegisterFactory("System.Data.SQLite", System.Data.SQLite.SQLiteFactory.Instance);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
#endif
        }
    }
}
