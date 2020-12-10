using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MultiRowExplorer.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace MultiRowExplorer
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Environment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public static IHostingEnvironment Environment { get; set; }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<C1NWindEntities>(o => o.UseSqlServer(GetConnectionString()));

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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var defaultCulture = "en-US";
            var supportedCultures = new[]
            {
                new CultureInfo(defaultCulture)
            };

            app.UseStaticFiles();
            app.UseSession();
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
            app.UseMvc(r => {
                r.MapRoute(
                    name: "Validation",
                    template: "{control}/UnobtrusiveValidation",
                    defaults: new { controller = "Validation", action = "Register" },
                    constraints: new { control = "(AutoComplete)|(ComboBox)|(MultiSelect)|(^Input.*)" }
                );

                r.MapRoute(
                    name: "default",
                    template: "{controller=MultiRow}/{action=Index}/{id?}");
            });
        }
    }
}
