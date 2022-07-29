using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public  ActionResult ExcelImportExportRTL()
        {
            return View(Models.Sale.GetData(500));
        }
    }
}
