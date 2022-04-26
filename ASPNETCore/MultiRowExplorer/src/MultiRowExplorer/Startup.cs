using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MultiRowExplorer.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
#if NETCORE31 || NET50
using Microsoft.Extensions.Hosting;
#endif

namespace MultiRowExplorer
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
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<C1NWindEntities>(o => o.UseSqlite(GetConnectionString()));

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

            var dataSourceText = "data source=";
            var index = configConnectionString.IndexOf(dataSourceText);
            if (index != -1)
            {
                return configConnectionString.Insert(index + dataSourceText.Length, dataFolderPath);
            }

            return configConnectionString;
        }

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

            app.UseStaticFiles();
            app.UseSession();

#if NETCORE31 || NET50
            app.UseRouting();
#endif

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
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
                    pattern: "{controller=MultiRow}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Validation",
                    pattern: "{control}/UnobtrusiveValidation",
                    defaults: new { controller = "Validation", action = "Register" },
                    constraints: new { control = "(AutoComplete)|(ComboBox)|(MultiSelect)|(^Input.*)" }
                );
            });
#else
            app.UseMvc(r => {
                r.MapRoute(
                    name: "default",
                    template: "{controller=MultiRow}/{action=Index}/{id?}");

                r.MapRoute(
                    name: "Validation",
                    template: "{control}/UnobtrusiveValidation",
                    defaults: new { controller = "Validation", action = "Register" },
                    constraints: new { control = "(AutoComplete)|(ComboBox)|(MultiSelect)|(^Input.*)" }
                );
            });
#endif
        }
    }
}
