using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvcExplorer.Models;
using System.Linq;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
#if ODATA_SERVER
#if NETCORE10
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.OData.Core;
#elif NETCORE20
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
#endif
#endif

#if NETCORE31
using Microsoft.Extensions.Hosting;
#endif

namespace MvcExplorer
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

        public IConfigurationRoot Configuration { get; set; }

#if NETCORE31
        public static IWebHostEnvironment Environment { get; set; }
#else
        public static IHostingEnvironment Environment { get; set; }
#endif

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<C1NWindEntities>(o => o.UseSqlite(GetConnectionString()));

#if ODATA_SERVER
#if NETCORE10
            services.AddOData<IODataService>(builder =>
            {
                builder.EntityType<Category>().HasKey(x => x.CategoryID);
                builder.EntityType<Order>().HasKey(x => x.OrderID);
            });
#elif NETCORE20
            services.AddOData();
#endif
#endif
            services.AddMvc()
#if NETCORE31
                .AddNewtonsoftJson()
#endif
            ;
#if NETCORE31
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
#endif
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

            return dataFolderPath + configConnectionString;
        }

#if NETCORE31
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

#if NETCORE31
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

#if ODATA_SERVER
#if NETCORE10
            // BUGS
            // Below fixes to make OData work properly.
            // None of that should be needed once Microsoft.AspNetCore.OData released.
            app.Use((context, func) =>
            {
                System.Diagnostics.Debug.WriteLine(context.Request.Path);

                if (context.Request.Path.StartsWithSegments(new PathString("/MyNorthWind"), StringComparison.OrdinalIgnoreCase))
                {
                    System.Diagnostics.Debug.WriteLine("Headers for {0} ({1}):", context.Request.Path, context.Request.Headers);

                    foreach (string key in context.Request.Headers.Keys)
                        System.Diagnostics.Debug.WriteLine(key + "=" + context.Request.Headers[key]);

                    // Case where only text/ plain and / or odata.metadata = minimal found in Accept header causing System.InvalidOperationException: No media types found in 'Microsoft.AspNetCore.OData.Formatter.ODataOutputFormatter.SupportedMediaTypes'.Add at least one media type to the list of supported media types.
                    // Solution is to enforce simple application/json content type
                    if (context.Request.Headers.ContainsKey("Accept"))
                    {
                        context.Request.Headers["Accept"] = "application/json";
                    }
                }

                // Fix to generate and return metadata
                // http://stackoverflow.com/questions/42193231/odata-v4-on-net-core-1-1-missing-metadata/43330412#43330412
                if (context.Request.Path.StartsWithSegments(new PathString("/MyNorthWind/$metadata"), StringComparison.OrdinalIgnoreCase))
                {
                    var edmModel = app.ApplicationServices.GetService<Microsoft.OData.Edm.IEdmModel>();

                    var stream = new MemoryStream();
                    var message = new InMemoryMessage() { Stream = stream };
                    var settings = new ODataMessageWriterSettings();

                    var writer = new ODataMessageWriter((IODataResponseMessage)message, settings, edmModel);
                    writer.WriteMetadataDocument();

                    string output = Encoding.UTF8.GetString(stream.ToArray());

                    return context.Response.WriteAsync(output);
                }

                return func();
            });

            // Configuring OData
            app.UseOData("MyNorthWind");
#elif NETCORE20
            // Configurate OData source.
            // http://odata.github.io/WebApi/#14-01-netcore-beta1
            var builder = new ODataConventionModelBuilder(app.ApplicationServices);
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<Order>("Orders");
#endif
#endif

#if NETCORE31
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Validation",
                    pattern: "{control}/UnobtrusiveValidation",
                    defaults: new { controller = "Validation", action = "Register" },
                    constraints: new { control = "(AutoComplete)|(ComboBox)|(MultiSelect)|(^Input.*)|(MultiAutoComplete)" }
                );
            });
#else

            app.UseMvc(r => {
#if ODATA_SERVER && !NETCORE10
                // Enable filter, order and count in OData
                r.Select().Expand().Filter().MaxTop(100).OrderBy().Count();
                r.MapODataServiceRoute(
                    routeName: "ODataRoute",
                    routePrefix: "MyNorthWind",
                    model: builder.GetEdmModel()
                );
                r.EnableDependencyInjection();
#endif
                r.MapRoute(
                    name: "Validation",
                    template: "{control}/UnobtrusiveValidation",
                    defaults: new { controller = "Validation", action = "Register" },
                    constraints: new { control = "(AutoComplete)|(ComboBox)|(MultiSelect)|(^Input.*)|(MultiAutoComplete)" }
                );

                r.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
#endif
        }
    }

#if ODATA_SERVER && NETCORE10
    public class InMemoryMessage : IODataRequestMessage, IODataResponseMessage
    {
        private readonly Dictionary<string, string> headers;

        public InMemoryMessage()
        {
            headers = new Dictionary<string, string>();
        }

        public IEnumerable<KeyValuePair<string, string>> Headers
        {
            get { return this.headers; }
        }

        public int StatusCode { get; set; }

        public Uri Url { get; set; }

        public string Method { get; set; }

        public Stream Stream { get; set; }

        public string GetHeader(string headerName)
        {
            string headerValue;
            return this.headers.TryGetValue(headerName, out headerValue) ? headerValue : null;
        }

        public void SetHeader(string headerName, string headerValue)
        {
            headers[headerName] = headerValue;
        }

        public Stream GetStream()
        {
            return this.Stream;
        }
    }
#endif
}
