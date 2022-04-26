using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class InputTimeController : Controller
    {
        public ActionResult CustomTime()
        {
            ViewBag.TimeList = new List<object> { "8:20", "10:00", "11:35", "12:08", "13:25", "13:30", "14:26" };
            return View();
        }
    }
}
