using System.IO;
#if ASPNETCORE
using Microsoft.AspNetCore.Mvc;
#else
using System.Web.Mvc;
#endif

namespace DashboardDemo.Controllers
{
    public partial class HomeController
    {
        private string GetDataFile(string[] folders)
        {
            foreach (string f in folders)
            {
                string filePath = Path.Combine(f, "InitialData.xml");
                if (System.IO.File.Exists(filePath)) return f;

                string cf = GetDataFile(Directory.GetDirectories(f));
                if (!string.IsNullOrEmpty(cf)) return cf;
            }
            return null;
        }

        public ActionResult Reporting()
        {
#if ASPNETCORE
            var reportFolder = DataXmlFolderPath ?? Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
#else
            var reportFolder = GetDataFile(new string[] { (Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory), "bin")) });
#endif
            C1.Win.FlexReport.C1FlexReport.DefaultBasePath = reportFolder;
            return View();
        }
    }
}
