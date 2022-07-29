using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
#if NETCORE31
using C1.AspNetCore.Api;
#endif

namespace CloudFileExplorer
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
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
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
            services.AddMvc()
#if NETCORE31
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
#else
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
#endif
                ;
            services.AddSession();

            // CORS support
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.Configure<FormOptions>(options => options.ValueLengthLimit = int.MaxValue);
#if NETCORE31
            services.AddC1Api();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#if NETCORE31
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#else
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
#endif
        {
#if NETCORE31
            app.UseC1Api();
#endif
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
#if !NETCORE31
            app.UseMvc();
#endif
            app.UseStaticFiles();
            app.UseSession();

#if NETCORE31
            app.UseRouting();
#endif

#region Storage registration
            string[] scopes = { DriveService.Scope.Drive };
            string applicationName = "C1WebApi";

#region Dropbox
            // Dropbox storage
            app.UseStorageProviders().AddDropBoxStorage("DropBox", Configuration["AppSettings:DropBoxStorageAccessToken"], applicationName);
#endregion

#region Onedrive
            //// Ondrive storage: uncomment these lines when want to test OneDrive storage
            //app.UseStorageProviders().AddOneDriveStorage("OneDrive", Configuration["AppSettings:OneDriveAccessToken"]);
#endregion

#region Google
            //// Google storage: please uncomment this line when you want to test GoogleDrive service.
            //app.UseStorageProviders().AddGoogleDriveStorage("GoogleDrive", GetUserCredential(scopes), applicationName);
#endregion

#region Local
            app.UseStorageProviders()
                .AddDiskStorage("ExcelRoot", Path.Combine(env.WebRootPath, "ExcelRoot"));
#endregion

#endregion

#if NETCORE31
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=FileManager}/{action=Index}/{id?}");
            });
#else
            app.UseMvc(r =>
            {
                r.MapRoute(
                    name: "default",
                    template: "{controller=FileManager}/{action=Index}/{id?}");
            });
#endif
        }

        private UserCredential GetUserCredential(string[] scopes)
        {
            UserCredential credential;

            using (var fileStream =
                    new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(fileStream).Secrets,
                        scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
            }
            return credential;
        }
    }
}
