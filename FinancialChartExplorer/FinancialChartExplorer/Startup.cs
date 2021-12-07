using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
#if NETCORE31 || NET50
using Microsoft.Extensions.Hosting;
#endif

namespace FinancialChartExplorer
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
            Environment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSession();
        }

        private string GetConnectionString()
        {
            var configConnectionString = Configuration["Data:DefaultConnection:ConnectionString"];
            const char folderSeparator = '\\';
            var dataFolderPath = Environment.WebRootPath.Replace('/', folderSeparator);
            if (dataFolderPath.Last() != folderSeparator)
            {
                dataFolderPath += folderSeparator;
            }

            var attachText = "AttachDbFilename=";
            var index = configConnectionString.IndexOf(attachText);
            if (index != -1)
            {
                return configConnectionString.Insert(index + attachText.Length, dataFolderPath);
            }

            return configConnectionString;
        }

#if NETCORE31 || NET50
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#else
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

#if NETCORE31 || NET50
            app.UseRouting();
#endif

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

#if NETCORE31 || NET50
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=HeikinAshi}/{id?}");

            });
#else
            app.UseMvc(r => {
                r.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=HeikinAshi}/{id?}");
            });
#endif
        }
    }
}
