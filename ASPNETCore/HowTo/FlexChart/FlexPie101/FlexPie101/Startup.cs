using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#if NETCORE31
using Microsoft.Extensions.Hosting;
#endif

namespace FlexPie101
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
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
#if NETCORE31
        public static IWebHostEnvironment Environment { get; set; }
#else
        public static IHostingEnvironment Environment { get; set; }
#endif

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add MVC services to the services container.
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

        // Configure is called after ConfigureServices is called.
#if NETCORE31
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#else
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
#endif
        {
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

            // Add MVC to the request pipeline.
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
