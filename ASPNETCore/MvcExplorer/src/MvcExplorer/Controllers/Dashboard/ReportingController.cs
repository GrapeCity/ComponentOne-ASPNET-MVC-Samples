using System.IO;
using C1.Report;

using Microsoft.AspNetCore.Mvc;


namespace MvcExplorer.Controllers
{
    public partial class DashboardController
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
            var reportFolder = DataXmlFolderPath ?? Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            FlexReport.DefaultBasePath = reportFolder;
            return View();
        }
    }
}
