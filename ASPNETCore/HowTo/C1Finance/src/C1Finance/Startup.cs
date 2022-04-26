using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#if NETCORE31
using Microsoft.Extensions.Hosting;
#endif

namespace C1Finance
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
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            var cacheFolder = System.IO.Path.Combine(env.WebRootPath, "Cache");
            if (!System.IO.Directory.Exists(cacheFolder))
            {
                System.IO.Directory.CreateDirectory(cacheFolder);
            }
            Models.PortfolioStatic.CacheFolder = cacheFolder;
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

            // Add application services.

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
