using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Web.Http;
using System.IO;
using System.Configuration;
using Google.Apis.Auth.OAuth2;
using System.Web;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Drive.v3;
using System;

[assembly: OwinStartupAttribute(typeof(CloudFileExplorer.Startup))]
namespace CloudFileExplorer
{
	public partial class Startup
    {
		private readonly HttpConfiguration config = GlobalConfiguration.Configuration;
		public void Configuration(IAppBuilder app)
        {
			app.UseCors(CorsOptions.AllowAll);
			
			#region Storage registration
			string[] scopes = { DriveService.Scope.Drive };
			string applicationName = "C1WebApi";

            #region Dropbox
            // Dropbox storage
            app.UseStorageProviders().AddDropBoxStorage("DropBox", ConfigurationManager.AppSettings["DropBoxStorageAccessToken"], applicationName);
            #endregion

            #region Onedrive
            //// Ondrive storage: uncomment these lines when want to test OneDrive storage
            //app.UseStorageProviders().AddOneDriveStorage("OneDrive", ConfigurationManager.AppSettings["OneDriveAccessToken"]);
            #endregion

            #region Azure
            //// Azure storage
            //app.UseStorageProviders().AddAzureStorage("Azure", ConfigurationManager.AppSettings["AzureStorageConnectionString"]);
            #endregion

            #region Google
            // Google storage: please uncomment this line when you want to test GoogleDrive service.
            //app.UseStorageProviders().AddGoogleDriveStorage("GoogleDrive", GetUserCredential(scopes), applicationName);
            #endregion

            #region AWS
            //         // AWS storage
            //         var aWSAccessToken = ConfigurationManager.AppSettings["AWSStorageAccessToken"];
            //var secretKey = ConfigurationManager.AppSettings["AWSStorageSecretKey"];
            //var bucketName = ConfigurationManager.AppSettings["AWSStorageBucketName"];
            //string region = "us-east-1";
            //app.UseStorageProviders().AddAWSStorage("AWS", aWSAccessToken, secretKey, bucketName, region);
            #endregion

            #region Local
            app.UseStorageProviders()
                .AddDiskStorage("ExcelRoot", GetFullRoot("ExcelRoot"));
            #endregion

            #endregion
        }

        private static string GetFullRoot(string root)
        {

            var applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var fullRoot = Path.GetFullPath(Path.Combine(applicationBase, root));
            if (!fullRoot.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
            {
                // When we do matches in GetFullPath, we want to only match full directory names.
                fullRoot += Path.DirectorySeparatorChar;
            }
            return fullRoot;
        }

        private UserCredential GetUserCredential(string[] scopes)
        {
            UserCredential credential;

            using (var fileStream =
                    new FileStream(HttpContext.Current.Server.MapPath("credentials.json"), FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(fileStream).Secrets,
                        scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(HttpContext.Current.Server.MapPath(credPath), true)).Result;
            }
            return credential;
        }
    }
}
