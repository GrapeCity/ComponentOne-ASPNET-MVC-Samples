using System.IO;
using System.Web.Mvc;

namespace DashboardDemo.Controllers
{
    public partial class HomeController
    {
        private string GetDataFile(string[] folders)
        {
            foreach(string f in folders)
            {
                var filePath = Path.Combine(f, "InitialData.xml");
                if (System.IO.File.Exists(filePath)) return f;

                var cf = GetDataFile(Directory.GetDirectories(f));
                if (!string.IsNullOrEmpty(cf)) return cf;
            }
            return null;
        }

        public ActionResult Reporting()
        {
            C1.Win.FlexReport.C1FlexReport.DefaultBasePath = DataXmlFolderPath ?? GetDataFile(new string[] { (Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory), "bin")) });
            return View();
        }
    }
}
